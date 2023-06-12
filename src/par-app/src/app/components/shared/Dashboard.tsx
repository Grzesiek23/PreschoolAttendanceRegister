import { Outlet } from 'react-router';
import { observer } from 'mobx-react-lite';
import TopBar from './TopBar';
import { Box, Typography } from '@mui/material';
import Sidebar from './Sidebar';
import Spinner from './Spinner';

function Dashboard() {
    return (
        <Box sx={{ display: 'flex' }}>
            <TopBar />
            <Sidebar />
            <Box sx={{ display: 'flex', flexDirection: 'column', width: '100%', minHeight: '100vh' }}>
                <Box component="main" sx={{ flexGrow: 1, px: 3, py: 1, mt: '64px' }}>
                    <Spinner />
                    <Outlet />
                </Box>
                <Box sx={{ flexGrow: 1, px: 3, py: 1, maxHeight: 40, mt: '64px', backgroundColor: '#dfdfe3', textAlign: 'center' }}>
                    <Typography>Grzesiek23 © 2023</Typography>
                </Box>
            </Box>
        </Box>
    );
}

export default observer(Dashboard);
