import { RouteObject } from 'react-router';
import { createBrowserRouter } from 'react-router-dom';
import App from '../../App';
import AuthLayout from '../components/shared/AuthLayout';
import RequireAuth from './RequireAuth';
import Home from "../../features/Home";
import UsersList from "../../features/Users";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '/login', element: <AuthLayout /> },
            {
                path: '/',
                element: <RequireAuth />,
                children: [
                    { path: 'home', element: <Home /> },
                    { path: 'users', element: <UsersList /> },
                    
                ],
            },
        ],
    },
];

export const router = createBrowserRouter(routes);
