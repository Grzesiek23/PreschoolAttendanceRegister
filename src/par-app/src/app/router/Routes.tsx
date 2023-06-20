import { RouteObject } from 'react-router';
import { createBrowserRouter } from 'react-router-dom';
import App from '../../App';
import AuthLayout from '../components/shared/AuthLayout';
import RequireAuth from './RequireAuth';
import Home from "../../features/Home";
import UsersList from "../../features/Users";
import UserForm from "../../features/Users/UserForm";
import UserEditForm from "../../features/Users/UserEditForm";
import UserDetails from "../../features/Users/Details";
import SchoolYearList from "../../features/SchoolYear";
import SchoolYearForm from "../../features/SchoolYear/SchoolYearForm";
import SchoolYearDetails from "../../features/SchoolYear/Details";
import GroupList from "../../features/Group";
import GroupDetails from "../../features/Group/Details";
import GroupForm from "../../features/Group/GroupForm";
import PreschoolerList from "../../features/Preschooler";
import PreschoolerForm from "../../features/Preschooler/PreschoolerForm";
import PreschoolerDetails from "../../features/Preschooler/Details";

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
                    { path: 'school-years', element: <SchoolYearList /> },
                    {
                        path: 'school-years/create',
                        element: <SchoolYearForm key="create" />,
                    },
                    { path: 'school-years/:id', element: <SchoolYearDetails /> },
                    { path: 'school-years/:id/edit', element: <SchoolYearForm key="manage" /> },

                    { path: 'groups', element: <GroupList /> },
                    {
                        path: 'groups/create',
                        element: <GroupForm key="create" />,
                    },
                    { path: 'groups/:id', element: <GroupDetails /> },
                    { path: 'groups/:id/edit', element: <GroupForm key="manage" /> },

                    { path: 'preschoolers', element: <PreschoolerList /> },
                    {
                        path: 'preschoolers/create',
                        element: <PreschoolerForm key="create" />,
                    },
                    { path: 'preschoolers/:id', element: <PreschoolerDetails /> },
                    { path: 'preschoolers/:id/edit', element: <PreschoolerForm key="manage" /> },
                    
                    
                    { path: 'users', element: <UsersList /> },
                    {
                        path: 'users/create',
                        element: <UserForm key="create" />,
                    },
                    { path: 'users/:id/edit', element: <UserEditForm key="manage" /> },
                    { path: 'users/:id', element: <UserDetails key="details" /> },
                ],
            },
        ],
    },
];

export const router = createBrowserRouter(routes);
