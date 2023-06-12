import { PaletteMode } from '@mui/material';
import { blue, grey, red } from '@mui/material/colors';

export const themeOptions = (mode: PaletteMode) => ({
    palette: {
        mode: mode,
        ...(mode === 'dark'
            ? {
                  // palette values for dark mode
                  primary: {
                      main: blue[800],
                  },
                  secondary: {
                      main: red[800],
                  },
                  neutral: {
                      main: grey[500],
                  },
                  background: {
                      default: grey[100],
                  },
              }
            : {
                  // palette values for light mode
                  primary: {
                      main: '#004c6d',
                      warning: '#f9a825',
                  },
                  secondary: {
                      main: '#437CB6',
                  },
                  neutral: {
                      main: grey[500],
                  },
                  background: {
                      default: grey[100],
                  },
              }),
    },
    typography: {
        fontFamily: ['Poppins'].join(','),
        fontSize: 14,
        fontWeight: 400,
        h1: {
            fontFamily: ['Poppins'].join(','),
            fontSize: 40,
            fontWeight: 500,
        },
        h2: {
            fontFamily: ['Poppins'].join(','),
            fontSize: 32,
            fontWeight: 500,
        },
        h3: {
            fontFamily: ['Poppins'].join(','),
            fontSize: 24,
            fontWeight: 500,
        },
        h4: {
            fontFamily: ['Poppins'].join(','),
            fontSize: 20,
            fontWeight: 500,
        },
        h5: {
            fontFamily: ['Poppins'].join(','),
            fontSize: 16,
            fontWeight: 500,
        },
        h6: {
            fontFamily: ['Poppins'].join(','),
            fontSize: 14,
            fontWeight: 700,
        },
        body1: {
            fontFamily: ['Poppins'].join(','),
            fontSize: 14,
            fontWeight: 500,
        },
    },
});
