import {useEffect} from 'react';
import { useParams } from 'react-router';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../app/stores/store';
import {Box, Button, Card, CardActions, CardContent, Checkbox, Grid, Typography} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { URL_CONSTANTS } from '../../app/consts/urlConstants';

function SchoolYearDetails() {
    const { schoolYearStore: store} = useStore();
    const { id } = useParams();
    const numId = parseInt(id ?? '');
    
    const navigate = useNavigate();

    useEffect(() => {
        (async () => {
            if (numId) {
                await store.loadSchoolYear(numId);
                if (store.schoolYear === undefined) navigate(URL_CONSTANTS.NOT_FOUND);
            } else {
                navigate(URL_CONSTANTS.NOT_FOUND);
            }
        })();
        return () => store.clearSchoolYear();
    }, [id, store]);

    return (
        <>
            {store.schoolYear && (
                <Card sx={{ minWidth: 275, maxWidth: 'sm' }}>
                    <CardContent>
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={10}>
                                <Typography variant="h4">Szczegóły użytkownika</Typography>
                            </Grid>
                            <Grid item xs={10} sm={10}>
                                <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                                    ID: #{store.schoolYear.id}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Nazwa:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.schoolYear.name}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Początek:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.schoolYear.startDate.toString()}
                                </Typography>
                            </Grid>

                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Koniec:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    {store.schoolYear.endDate.toString()}
                                </Typography>
                            </Grid>
                            
                            <Grid item xs={12} sm={6}>
                                <Typography variant="body1" component="div">
                                    Obecny rok szkolny:
                                </Typography>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <Typography variant="h6" component="div">
                                    <Checkbox checked={store.schoolYear.isCurrent} disabled={true} sx={{p: 0}} />
                                </Typography>
                            </Grid>
                        </Grid>
                    </CardContent>
                    <CardActions>
                        <Box display="flex" justifyContent="space-between" width="100%">
                            <Button size="small" onClick={() => navigate(URL_CONSTANTS.SCHOOL_YEARS)}>
                                Wróć
                            </Button>
                            <Box>
                                <Button
                                    size="small"
                                    variant="contained"
                                    onClick={() => {
                                        navigate(URL_CONSTANTS.SCHOOL_YEARS_EDIT(numId));
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

export default observer(SchoolYearDetails);
