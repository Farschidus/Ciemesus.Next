module.exports = {
    plugins: ['ramda'],
    extends: ['react-app', 'airbnb', 'plugin:ramda/recommended'],
    rules: {
        indent: [
            // Indent 4 spaces
            'error',
            4,
            {
                SwitchCase: 1,
            },
        ],
        'no-empty': ['error', { "allowEmptyCatch": true }], // Catch is often not handled
        'max-len': 'off', // Eslint is unable to autofix this rule in some cases which makes it hard to use
        'no-use-before-define': 'off', // To define style and translation  after component
        'no-shadow': 'off', // To use same variable in nested scope
        'react/jsx-filename-extension': 'off', // Don't want to specify .js in imports
        'react/jsx-indent': ['error', 4], // Indent 4 spaces
        'react/jsx-indent-props': ['error', 4], // Indent 4 spaces
        'react/prop-types': 'off', // Don't want to specify propTypes
        'react/style-prop-object': 'off', // style prop expects string in FormattedList
        'react/state-in-constructor': 'off',
        'react/jsx-props-no-spreading': 'off', // Rule should be used, but we use spreads too much wich makes it hard to turn on
        'import/no-unresolved': ['error', { ignore: ['^react-native$'] }],
    },
};
