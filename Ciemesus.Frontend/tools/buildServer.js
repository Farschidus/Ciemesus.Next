/* eslint-disable no-console */
import webpack from 'webpack';
import webpackProdConfig from '../webpack.prod';
import { chalkError, chalkSuccess, chalkWarning, chalkProcessing } from './chalkConfig';

const config = webpack(webpackProdConfig);

console.log(chalkProcessing('Generating minified bundle. This will take a moment...'));

webpack(config).run((error, stats) => {
    if (error) { // so a fatal error occurred. Stop here.
        console.log(chalkError(error));
        return 1;
    }

    const jsonStats = stats.toJson();

    if (jsonStats.hasErrors) {
        return jsonStats.errors.map((jsonStatsError) => console.log(chalkError(jsonStatsError)));
    }

    if (jsonStats.hasWarnings) {
        console.log(chalkWarning('Webpack generated the following warnings: '));
        jsonStats.warnings.map((warning) => console.log(chalkWarning(warning)));
    }

    console.log(`Webpack stats: ${stats}`);

    // if we got this far, the build succeeded.
    console.log(chalkSuccess('Your app is compiled in production mode in /dist. It\'s ready to roll!'));

    return 0;
});
