module.exports = {
    theme: {
        fontFamily: {
            body: ['CrimsonPro', 'serif'],
            brand: ['NotoSerifJP Bold', 'serif']
        },
        extend: {
            colors: {
                primary: {
                    lighter: '#5B78AE',
                    light: '#3B5C98',
                    default: '#21468B',
                    dark: '#14346F',
                    darker: '#072355'
                },
                accent: {
                    lighter: '#7161B4',
                    light: '#52409D',
                    default: '#3A2590',
                    dark: '#291774',
                    darker: '#190A5B'
                }
            }
        }
    },
    variants: {
        borderRadius: ({ after }) => after(['first', 'last']),
    }
};
