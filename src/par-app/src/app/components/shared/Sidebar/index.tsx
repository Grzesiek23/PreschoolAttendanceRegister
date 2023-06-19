import React from 'react';
import HomeIcon from '@mui/icons-material/Home';
import { observer } from 'mobx-react-lite';
import { Divider, Theme, CSSObject, Typography, Box } from '@mui/material';
import MuiDrawer from '@mui/material/Drawer';
import { styled } from '@mui/material/styles';
import { useStore } from '../../../stores/store';
import { NavLink } from 'react-router-dom';
import { URL_CONSTANTS } from '../../../consts/urlConstants';
import ApiIcon from '@mui/icons-material/Api';
import PeopleAltIcon from '@mui/icons-material/PeopleAlt';

const drawerWidth = 250;
const openedMixin = (theme: Theme): CSSObject => ({
    width: drawerWidth,
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
    }),
    overflowX: 'hidden',
});

const closedMixin = (theme: Theme): CSSObject => ({
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: `calc(${theme.spacing(7)} + 1px)`,
    [theme.breakpoints.up('sm')]: {
        width: `calc(${theme.spacing(8)} + 1px)`,
    },
});

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(({ theme, open }) => ({
    width: drawerWidth,
    flexShrink: 0,
    whiteSpace: 'nowrap',
    boxSizing: 'border-box',
    ...(open && {
        ...openedMixin(theme),
        '& .MuiDrawer-paper': openedMixin(theme),
    }),
    ...(!open && {
        ...closedMixin(theme),
        '& .MuiDrawer-paper': closedMixin(theme),
    }),
}));

const MenuItem = ({ icon: Icon, to, label }: { icon: React.ReactElement; to: string; label: string }) => {
    return (
        <Box
            component={NavLink}
            to={to}
            sx={{
                display: 'flex',
                flexDirection: 'column',
                pl: 1.5,
                py: 0.5,
                mx: 1,
                my: 1,
                borderRadius: 2,
                color: 'rgba(255, 255, 255, 0.8)',
                textDecoration: 'none !important',
                '&:hover': {
                    backgroundColor: 'rgba(255, 255, 255, 0.24)',
                },
                '&.active': {
                    backgroundColor: 'rgb(255, 255, 255, 0.15)',
                },
            }}>
            <Box sx={{ display: 'flex', alignItems: 'center', my: 1 }}>
                {Icon}
                <Typography sx={{ ml: 3, fontSize: 13 }}>{label}</Typography>
            </Box>
        </Box>
    );
};

function Sidebar() {
    const { commonStore } = useStore();

    return (
        <Drawer variant="permanent" open={commonStore.sidebarOpen} PaperProps={{ sx: { backgroundColor: '#353A40' } }}>
            <Box sx={{ marginTop: '64px' }}>
                <MenuItem icon={<HomeIcon />} to="/home" label="Strona główna" />
                <Divider sx={{ backgroundColor: 'gray', mx: 2 }} />
                <MenuItem icon={<PeopleAltIcon />} to={URL_CONSTANTS.PRESCHOOLERS} label="Dzieci" />
                <MenuItem icon={<ApiIcon />} to={URL_CONSTANTS.GROUPS} label="Grupy" />
                <MenuItem icon={<ApiIcon />} to={URL_CONSTANTS.USERS} label="Użytkownicy" />
                <MenuItem icon={<ApiIcon />} to={URL_CONSTANTS.SCHOOL_YEARS} label="Roczniki" />
                <MenuItem icon={<ApiIcon />} to={URL_CONSTANTS.GROUPS} label="Grupy" />
            </Box>
        </Drawer>
    );
}

export default observer(Sidebar);
