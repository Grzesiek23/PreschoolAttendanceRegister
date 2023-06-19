import ReactDOM from 'react-dom/client'
import './index.css'
import 'react-toastify/dist/ReactToastify.css';
import {createTheme, CssBaseline, ThemeProvider} from "@mui/material";
import {LocalizationProvider} from "@mui/x-date-pickers";
import { store, StoreContext } from './app/stores/store';
import {themeOptions} from "./app/theme";
import {plPL} from "@mui/x-data-grid";
import { plPL as corePlPL } from '@mui/material/locale';
import {RouterProvider} from "react-router-dom";
import {router} from "./app/router/Routes";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";

const theme = createTheme(themeOptions('light'), plPL, corePlPL);

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <ThemeProvider theme={theme}>
        <CssBaseline />
        <StoreContext.Provider value={store}>
            <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="pl">
                <RouterProvider router={router} />
            </LocalizationProvider>
        </StoreContext.Provider>
    </ThemeProvider>
)
