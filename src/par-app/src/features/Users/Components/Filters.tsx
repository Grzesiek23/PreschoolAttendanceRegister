import { observer } from 'mobx-react-lite';
import { Box, Button, Divider, Grid } from '@mui/material';
import { useStore } from '../../../app/stores/store';
import ClearableTextField from '../../../app/components/shared/Controls/ClearableTextField';
import { useNavigate } from 'react-router-dom';

function Filters() {
    const { userStore: store } = useStore();

    const onFilterFirstNameChange = async (e: React.ChangeEvent<HTMLInputElement>): Promise<void> => {
        await store.setFilterFirstName(e.target.value);
    };

    const onFilterLastNameChange = async (e: React.ChangeEvent<HTMLInputElement>): Promise<void> => {
        await store.setFilterLastName(e.target.value);
    };

    const onFilterEmailChange = async (e: React.ChangeEvent<HTMLInputElement>): Promise<void> => {
        await store.setFilterEmail(e.target.value);
    };

    const navigate = useNavigate();

    return (
        <>
            <Grid container spacing={2}>
                <Grid item sm={12} md={2}>
                    <ClearableTextField
                        fullWidth={true}
                        onClearClick={() => store.setFilterFirstName('')}
                        onChange={onFilterFirstNameChange}
                        value={store.filterFirstName}
                        label="Imię"
                        size="small"
                    />
                </Grid>
                <Grid item sm={12} md={2}>
                    <ClearableTextField
                        fullWidth={true}
                        onClearClick={() => store.setFilterLastName('')}
                        onChange={onFilterLastNameChange}
                        value={store.filterLastName}
                        label="Nazwisko"
                        size="small"
                    />
                </Grid>
                <Grid item sm={12} md={2}>
                    <ClearableTextField
                        fullWidth={true}
                        onClearClick={() => store.setFilterEmail('')}
                        onChange={onFilterEmailChange}
                        value={store.filterEmail}
                        label="Adres e-mail"
                        size="small"
                    />
                </Grid>
            </Grid>
            <Divider sx={{ my: 2 }} />
            <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                <Button variant="contained" color="primary" onClick={() => store.resetFilters()}>
                    Resetuj filtry
                </Button>
                <Button variant="contained" color="success" onClick={() => navigate('/users/create')}>
                    Dodaj użytkownika
                </Button>
            </Box>
        </>
    );
}

export default observer(Filters);
