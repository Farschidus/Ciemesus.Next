using System.IO;
using System.Threading.Tasks;
using Ciemesus.Core.Data;
using Ciemesus.Core.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Ciemesus.Core.Ciemesus
{
    public class CiemesusImageGet
    {
        public class Command : ICiemesusRequest<ICiemesusResponse<CommandResult>>
        {
            public int CiemesusId { get; set; }

            public string FilePath { get; set; }

            public IFormFile ImageFile { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CiemesusDb db)
            {
                CascadeMode = CascadeMode.StopOnFirstFailure;

                RuleFor(c => c.CiemesusId)
                    .NotEmpty()
                    .CiemesusExists(db);

                RuleFor(c => c.ImageFile)
                    .NotEmpty()
                    .Must(f => f.Length > 0);
            }
        }

        public class CommandResult
        {
            public string Result { get; set; }
        }

        public class CommandHandler : ICiemesusAsyncRequestHandler<Command, ICiemesusResponse<CommandResult>>
        {
            private readonly CiemesusDb _db;

            public CommandHandler(CiemesusDb db)
            {
                _db = db;
            }

            public async Task<ICiemesusResponse<CommandResult>> Handle(Command message)
            {
                var imageFile = message.ImageFile.FileName;
                var extention = Path.GetExtension(imageFile).ToLower();

                var path = Path.Combine(message.FilePath, message.CiemesusId.ToString() + extention);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await message.ImageFile.CopyToAsync(stream);
                }

                var fika = await _db.Subject.FindAsync(message.CiemesusId);
                int index = path.IndexOf("\\clientImages");
                fika.Pics = path.Substring(index).Replace("\\", "/");

                await _db.SaveChangesAsync();
                var result = new CommandResult
                {
                    Result = "Well Done"
                };

                return new CiemesusResponse<CommandResult>
                {
                    Result = result
                };
            }
        }
    }
}
