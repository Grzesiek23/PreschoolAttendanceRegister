import { RouteObject } from 'react-router';
import { createBrowserRouter } from 'react-router-dom';
import App from '../../App';
import AuthLayout from '../components/shared/AuthLayout';
import RequireAuth from './RequireAuth';
import Home from "../../features/Home";
import UsersList from "../../features/Users";
import UserForm from "../../features/Users/UserForm";
import UserEditForm from "../../features/Users/UserEditForm";

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
                    {
                        path: 'users/create',
                        element: <UserForm key="create" />,
                    },
                    { path: 'users/:id/edit', element: <UserEditForm key="manage" /> },
                ],
            },
        ],
    },
];

export const router = createBrowserRouter(routes);
