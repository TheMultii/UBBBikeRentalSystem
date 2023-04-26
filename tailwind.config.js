/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml',
        './Areas/Identity/Pages/**/*.cshtml',
        './Areas/Admin/Views/**/*.cshtml'
    ],
    theme: {
        screens: {
            '2xl': { 'max': '1535px' },
            'xl': { 'max': '1399px' },
            'lg': { 'max': '1199px' },
            'md': { 'max': '991px' },
            'sm': { 'max': '767px' },
            'col': { 'max': '567px' }
        },
        extend: {},
    },
    plugins: [],
}