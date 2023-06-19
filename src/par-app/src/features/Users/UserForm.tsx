import {
    Box,
    Button,
    Card,
    CardContent,
    Checkbox,
    FormControl,
    FormControlLabel,
    InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    Typography,
} from '@mui/material';
import { Form, Formik } from 'formik';
import TextField from '@mui/material/TextField';
import { URL_CONSTANTS } from '../../app/consts/urlConstants';
import { useEffect, useState } from 'react';
import { ApplicationUserFormValues } from '../../app/models/applicationUser';
import * as Yup from 'yup';
import { useStore } from '../../app/stores/store';
import { useNavigate } from 'react-router-dom';
import { onlyDigits } from '../../app/utils/validators';
import { toast } from 'react-toastify';

const validationSchema = Yup.object({
    email: Yup.string().required('Email jest wymagany').email('Niepoprawny format email'),
    password: Yup.string()
        .required('Hasło jest wymagane')
        .min(6, 'Hasło musi mieć minimum 6 znaków')
        .matches(
            /^(?=.*[0-9])(?=.*[A-Z])(?=.*[!@#$%^&*])/,
            'Hasło musi zawierać przynajmniej jedną liczbę, jedną dużą literę i jeden znak specjalny',
        ),
    confirmPassword: Yup.string()
        .required('Hasło jest wymagane')
        .nullable()
        .oneOf([Yup.ref('password'), null], 'Hasła muszą być takie same'),
    firstName: Yup.string().required('Imię jest wymagane'),
    lastName: Yup.string().required('Nazwisko jest wymagane'),
    phoneNumber: Yup.string().required('Numer telefonu jest wymagany').min(9, 'Numer telefonu musi mieć 9 cyfr'),
});

function UserForm() {
    const [user, setUser] = useState<ApplicationUserFormValues>(new ApplicationUserFormValues());
    const [emailError, setEmailError] = useState<string | ''>();
    const { userStore: store, roleStore } = useStore();
    const navigate = useNavigate();

    const handleSubmit = async (values: ApplicationUserFormValues): Promise<void> => {
        const success = await store.createUser(values);
        if (success) {
            navigate(URL_CONSTANTS.USERS_DETAILS(store.createdUserId));
            toast.success('Dodano nowego użytkownika');
        }
    };
    
    const validateEmailAsync = async (email: string) => {
        const emailSchema = Yup.string()
            .required('Email jest wymagany')
            .email('Niepoprawny format email');
        try {
            await emailSchema.validate(email);
            const exists = await store.exists(email);
            if (exists) {
                setEmailError('Email jest już zajęty');
            } else {
                setEmailError(undefined);
            }
        } catch (error) {
            if (error instanceof Yup.ValidationError) {
                setEmailError(error.message);
            } else {
                setEmailError('Wystąpił nieoczekiwany błąd');
            }
        }
    }

    useEffect(() => {
        (async () => {
            await roleStore.loadRoles();
            const role = roleStore.roles?.find((role) => role.name === 'User');
            if (role) {
                setUser((user) => ({ ...user, role: role.id }));
            }
        })();
        
        return () => store.clearUser();
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
                                value={values.email}
                                onChange={handleChange}
                                size="small"
                                onBlur={async (e) => {
                                    handleBlur(e);
                                    await validateEmailAsync(values.email);
                                }}
                                error={touched.email && Boolean(emailError)}
                                helperText={touched.email && emailError}
                                autoComplete="username"
                            />

                            <TextField
                                name="password"
                                type="password"
                                label="Hasło"
                                value={values.password}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.password && Boolean(errors.password)}
                                helperText={touched.password && errors.password}
                                size="small"
                                autoComplete="new-password"
                            />

                            <TextField
                                name="confirmPassword"
                                type="password"
                                label="Powtórz hasło"
                                value={values.confirmPassword}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.confirmPassword && Boolean(errors.confirmPassword)}
                                helperText={touched.confirmPassword && errors.confirmPassword}
                                size="small"
                                autoComplete="new-password"
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

                            {roleStore.roles && user.role !== '' && (
                                <FormControl>
                                    <InputLabel id="roleLabel">Wybierz rolę</InputLabel>
                                    <Select
                                        name="role"
                                        labelId="roleLabel"
                                        label="Wybierz rolę"
                                        onChange={handleChange}
                                        value={values.role}
                                        size="small">
                                        {roleStore.roles?.map((name) => (
                                            <MenuItem key={name.id} value={name.id}>
                                                {name.name}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            )}

                            <FormControlLabel
                                control={
                                    <Checkbox
                                        name="sendEmail"
                                        value={values.sendEmail}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        checked={values.sendEmail}
                                    />
                                }
                                label="Wyślij email z danymi logowania"
                            />

                            <Box mt={2}>
                                <Button type="submit" variant="contained" color="primary">
                                    Utwórz konto
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
