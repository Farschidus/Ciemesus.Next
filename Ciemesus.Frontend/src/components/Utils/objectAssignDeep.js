function isObject(item) {
    return !item ? false : (item && typeof item === 'object' && !Array.isArray(item));
}

function objectAssignDeep(target, ...sources) {
    if (!sources.length) {
        return target;
    }

    const source = sources.shift();

    if (isObject(target) && isObject(source)) {
        Object.keys(source).forEach((key) => {
            if (isObject(source[key])) {
                if (!target[key]) {
                    Object.assign(target, { [key]: {} });
                }

                objectAssignDeep(target[key], source[key]);
            } else {
                Object.assign(target, { [key]: source[key] });
            }
        });
    }

    return objectAssignDeep(target, ...sources);
}

export default objectAssignDeep;
