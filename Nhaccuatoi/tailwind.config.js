/** @type {import('tailwindcss').Config} */
module.exports = {
  purge: {
    enabled: true,
    content: [ 
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml'
    ]
  },
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      fontFamily: {
        'inter': ['Inter', 'sans-serif'],
      },
      animation: {
        'spin-slow': 'spin 20s linear infinite',
        // 'shimmer': 'shimmer 10s ease-in-out infinite',
      },
      backgroundImage: {
        'selected': "linear-gradient(90deg, rgba(255, 255, 255, 0.6) 0%, rgba(255, 255, 255, 0.2) 20%, rgba(255, 255, 255, 0.4) 60%, rgba(255, 255, 255, 0) 100% )"
      },
      // keyframes: {
      //   shimmer: {
      //     '100%': {
      //       transform: 'translateX(100%)',
      //     }
      //   }
      // },
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}

