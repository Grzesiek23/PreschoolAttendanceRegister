import {
    Box,
    Button,
    Card,
    CardContent,
    FormControl,
    InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    Typography
} from '@mui/material';
import { Form, Formik } from 'formik';
import TextField from '@mui/material/TextField';
import { URL_CONSTANTS } from '../../app/consts/urlConstants';
import { useEffect, useState } from 'react';
import { ApplicationUserEditFormValues } from '../../app/models/applicationUser';
import * as Yup from 'yup';
import { useStore } from '../../app/stores/store';
import { useNavigate } from 'react-router-dom';
import { onlyDigits } from '../../app/utils/validators';
import { toast } from 'react-toastify';
import { useParams } from 'react-router';

const validationSchema = Yup.object({
    email: Yup.string().required('Email jest wymagany').email('Niepoprawny format email'),
    firstName: Yup.string().required('Imię jest wymagane'),
    lastName: Yup.string().required('Nazwisko jest wymagane'),
});

function UserForm() {
    const { id } = useParams();
    const [user, setUser] = useState<ApplicationUserEditFormValues>(new ApplicationUserEditFormValues());
    const [emailError] = useState<string | undefined>();
    const { userStore: store, roleStore } = useStore();
    const navigate = useNavigate();

    const handleSubmit = async (values: ApplicationUserEditFormValues): Promise<void> => {
        const success = await store.updateUser(values);
        if (success) {
            navigate(URL_CONSTANTS.USERS_DETAILS(store.user?.id!));
            toast.success('Użytkownik został zaktualizowany');
        }
    };

    useEffect(() => {
        (async () => {
            await roleStore.loadRoles();

            if (id) {
                await store.loadUser(id);
                setUser(new ApplicationUserEditFormValues(store.user));

                const role = roleStore.roles?.find((role) => role.name === store.user?.role?.name);
                if (role) {
                    setUser((user) => ({ ...user, role: role.id }));
                }
            }
        })();
    }, []);

    return (
        <Card>
            <Typography variant="h4" sx={{ ml: 2, mt: 2 }}>
                {user.id ? 'Edycja użytkownika' : 'Dodaj użytkownika'}
            </Typography>
            <CardContent>
                <Formik
                    validationSchema={validationSchema}
                    initialValues={user}
                    onSubmit={async (values) => {
                        if (emailError) {
                            toast.warning('Niepoprawny email');
                        } else {
                            await handleSubmit(values);
                        }
                    }}
                    enableReinitialize>
                    {({ values, handleChange, handleBlur, touched, errors }) => (
                        <Form style={{ display: 'flex', flexDirection: 'column', gap: 15 }}>
                            <TextField
                                name="email"
                                label="Email"
                                value={store.user?.email || ''}
                                size="small"
                                autoComplete="username"
                                disabled={true}
                            />

                            <TextField
                                name="firstName"
                                label="Imię"
                                value={values.firstName}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.firstName && Boolean(errors.firstName)}
                                helperText={touched.firstName && errors.firstName}
                                size="small"
                            />

                            <TextField
                                name="lastName"
                                label="Nazwisko"
                                value={values.lastName}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.lastName && Boolean(errors.lastName)}
                                helperText={touched.lastName && errors.lastName}
                                size="small"
                            />

                            <TextField
                                id="phoneNumber"
                                size="small"
                                onKeyDown={onlyDigits}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values.phoneNumber}
                                error={touched.phoneNumber && Boolean(errors.phoneNumber)}
                                helperText={touched.phoneNumber && errors.phoneNumber}
                                sx={{ my: 1, width: 250 }}
                                inputProps={{ maxLength: 9 }}
                                InputProps={{
                                    startAdornment: <InputAdornment position="start">+48</InputAdornment>,
                                }}
                            />

                            {roleStore.roles && roleStore.roles.length > 0 && user.roleId !== '' && (
                                <FormControl>
                                    <InputLabel id="roleLabel">Wybierz rolę</InputLabel>
                                    <Select
                                        name="roleId"
                                        labelId="roleLabel"
                                        label="Wybierz rolę"
                                        onChange={handleChange}
                                        value={values.roleId}
                                        size="small">
                                        {roleStore.roles?.map((name) => (
                                            <MenuItem key={name.id} value={name.id}>
                                                {name.name}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            )}

                            <Box mt={2}>
                                <Button type="submit" variant="contained" color="primary">
                                    Zapisz zmiany
                                </Button>
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    sx={{ ml: 3 }}
                                    onClick={() => {
                                        store.clearUser();
                                        navigate(URL_CONSTANTS.USERS);
                                    }}>
                                    Anuluj
                                </Button>
                            </Box>
                        </Form>
                    )}
                </Formik>
            </CardContent>
        </Card>
    );
}

export default UserForm;
