import {useEffect} from 'react';
import { useParams } from 'react-router';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../app/stores/store';
import {Box, Button, Card, CardActions, CardContent, Grid, Typography} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { URL_CONSTANTS } from '../../app/consts/urlConstants';

function PreschoolerDetails() {
    const { preschoolerStore: store} = useStore();
    const { id } = useParams();
    const numId = parseInt(id ?? '');
    
    const navigate = useNavigate();

    useEffect(() => {
        (async () => {
            if (numId) {
                await store.loadPreschooler(numId);
                if (store.preschooler === undefined) navigate(URL_CONSTANTS.NOT_FOUND);
            } else {
                navigate(URL_CONSTANTS.NOT_FOUND);
            }
        })();
        return () => store.clearPreschooler();
    }, [id, store]);

    return (
        <>
            {store.preschooler && (
                <Card sx={{ minWidth: 275, maxWidth: 'sm' }}>
                    <CardContent>
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={10}>
                                <Typography variant="h4">Szczegóły dziecka</Typography>
                            </Grid>
                            <Grid item xs={10} sm={10}>
                                <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                                    ID: #{store.preschooler.id}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Imię:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.preschooler.firstName}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Nazwisko:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.preschooler.lastName}
                                </Typography>
                            </Grid>

                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Grupa:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.preschooler.group?.name}
                                </Typography>
                            </Grid>
                        </Grid>
                    </CardContent>
                    <CardActions>
                        <Box display="flex" justifyContent="space-between" width="100%">
                            <Button size="small" onClick={() => navigate(URL_CONSTANTS.PRESCHOOLERS)}>
                                Wróć
                            </Button>
                            <Box>
                                <Button
                                    size="small"
                                    variant="contained"
                                    onClick={() => {
                                        navigate(URL_CONSTANTS.PRESCHOOLERS_EDIT(numId));
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

export default observer(PreschoolerDetails);
