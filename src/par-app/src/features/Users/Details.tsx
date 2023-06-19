import { useEffect } from 'react';
import { useParams } from 'react-router';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../app/stores/store';
import { Box, Button, Card, CardActions, CardContent, Grid, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { URL_CONSTANTS } from '../../app/consts/urlConstants';

function UserDetails() {
    const { userStore: store } = useStore();
    const { id } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        (async () => {
            if (id) {
                await store.loadUser(id);
                if (store.user === undefined) navigate(URL_CONSTANTS.NOT_FOUND);
            } else {
                navigate(URL_CONSTANTS.NOT_FOUND);
            }
        })();
        return () => store.clearUser();
    }, [id, store]);

    return (
        <>
            {store.user && (
                <Card sx={{ minWidth: 275, maxWidth: 'sm' }}>
                    <CardContent>
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={10}>
                                <Typography variant="h4">Szczegóły użytkownika</Typography>
                            </Grid>
                            <Grid item xs={10} sm={10}>
                                <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                                    ID: #{store.user.id}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Email:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.user.email ?? 'Brak'}
                                </Typography>
                            </Grid>

                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Imię i nazwisko:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.user.firstName} {store.user.lastName}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Telefon:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.user.phoneNumber ?? 'Brak'}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Rola:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.user.role ? store.user.role.name : 'Brak'}
                                </Typography>
                            </Grid>
                        </Grid>
                    </CardContent>
                    <CardActions>
                        <Box display="flex" justifyContent="space-between" width="100%">
                            <Button size="small" onClick={() => navigate(URL_CONSTANTS.USERS)}>
                                Wróć
                            </Button>
                            <Box>
                                <Button
                                    size="small"
                                    variant="contained"
                                    onClick={() => {
                                        navigate(URL_CONSTANTS.USERS_EDIT_PASSWORD(id ?? ''));
                                    }}>
                                    Zmień hasło
                                </Button>

                                <Button
                                    size="small"
                                    variant="contained"
                                    sx={{ mx: 1.25 }}
                                    onClick={() => {
                                        navigate(URL_CONSTANTS.USERS_EDIT_EMAIL(id ?? ''));
                                    }}>
                                    Zmień adres email
                                </Button>

                                <Button
                                    size="small"
                                    variant="contained"
                                    onClick={() => {
                                        navigate(URL_CONSTANTS.USERS_EDIT(id ?? ''));
                                    }}>
                                    Edytuj
                                </Button>
                            </Box>
                        </Box>
                    </CardActions>
                </Card>
            )}
        </>
    );
}

export default observer(UserDetails);
