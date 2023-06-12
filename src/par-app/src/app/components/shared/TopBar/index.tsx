import React, { useState } from 'react';
import {Box, Divider, IconButton, Menu, MenuItem, Typography} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import LogoutIcon from '@mui/icons-material/Logout';
import Toolbar from '@mui/material/Toolbar';
import { useStore } from '../../../stores/store';
import SettingsIcon from '@mui/icons-material/Settings';
import { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar/AppBar';
import { styled } from '@mui/material/styles';
import MuiAppBar from '@mui/material/AppBar';
import { useNavigate } from 'react-router-dom';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

interface AppBarProps extends MuiAppBarProps {
    open?: boolean;
}

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    ...(open && {
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
    }),
}));

export default function TopBar() {
    const { accountStore, commonStore } = useStore();
    const navigate = useNavigate();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleLogout = () => {
        accountStore.logoutUser();
        navigate('/login');
    };

    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <AppBar position="fixed" open={commonStore.sidebarOpen}>
            <Toolbar sx={{ justifyContent: 'space-between' }}>
                <Box display="flex">
                    <Box
                        component="img"
                        src="/images/par-logo.png"
                        sx={{
                            height: 40,
                            color: 'white',
                        }}
                    />
                    <Typography sx={{ ml: 2, mt: 1}} variant='h4'
                    >Dzienniczek</Typography>
                    <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        onClick={() => commonStore.toggleSidebar()}
                        edge="start"
                        sx={{
                            marginLeft: 6,
                        }}>
                        <MenuIcon />
                    </IconButton>
                </Box>
                <Box display="flex">
                    <IconButton
                        sx={{ color: 'white' }}
                        aria-controls={open ? 'basic-menu' : undefined}
                        aria-haspopup="true"
                        aria-expanded={open ? 'true' : undefined}
                        onClick={handleClick}>
                        <SettingsIcon />
                    </IconButton>
                    <Divider
                        orientation="vertical"
                        sx={{ backgroundColor: 'white', opacity: 0.5, my: 0.5, mx: 1 }}
                        flexItem
                    />
                    <IconButton sx={{ color: 'white' }} onClick={handleLogout}>
                        <LogoutIcon />
                    </IconButton>
                </Box>
                <Menu
                    id="basic-menu"
                    anchorEl={anchorEl}
                    open={open}
                    onClose={handleClose}
                    MenuListProps={{
                        'aria-labelledby': 'basic-button',
                    }}>
                    <MenuItem onClick={handleClose}>
                        <AccountCircleIcon sx={{ marginRight: 1 }} />
                        Mój profil
                    </MenuItem>
                </Menu>
            </Toolbar>
        </AppBar>
    );
}
