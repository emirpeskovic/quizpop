/** @type {import('tailwindcss').Config} */
module.exports = {
    purge: [
        './Views/**/*.cshtml',
        './wwwroot/js/**/*.js'
    ],
    theme: {
        extend: {}
    },
    variants: {},
    plugins: [
        require('postcss-import'),
        require('tailwindcss'),
        require('autoprefixer'),
        require('cssnano')({
            preset: 'default',
        }),
        require('@tailwindcss/typography'),
        require('@tailwindcss/forms'),
        require('@tailwindcss/line-clamp'),
        require('@tailwindcss/aspect-ratio'),
    ],
};