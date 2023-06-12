import { observer } from 'mobx-react-lite';
import { Box, Button, Container, InputAdornment, TextField, Typography } from '@mui/material';
import { ErrorMessage, Form, Formik } from 'formik';
import {useStore} from "../../app/stores/store";
import {useLocation, useNavigate} from "react-router-dom";
import {useEffect} from "react";

export default observer(function Login() {
    const store = useStore();
    const { accountStore } = store;
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        if (accountStore.isLoggedIn()) {
            const { from } = (location.state as any) || { from: { pathname: '/home' } };
            navigate(from);
        }
    }, [accountStore.user, navigate, location]);
    
    return (
        <Container sx={{ width: '460px' }}>
            <Box
                sx={{
                    backgroundColor: 'white',
                    flexGrow: 1,
                    borderRadius: 3,
                    mt: { xs: 4, md: 12 },
                }}>
                <Box
                    sx={{
                        backgroundImage: "url('/images/kids.jpg')",
                        backgroundSize: 'cover',
                        borderRadius: '12px 12px 0 0',
                    }}
                    display="flex"
                    justifyContent="space-between">
                    <Box component="img" sx={{ height: '200px' }} />
                </Box>

                <Box sx={{ my: 3, textAlign: 'center' }}>
                    <Typography variant="h5" sx={{ fontSize: '1rem' }}>
                        Witaj w dzienniczku obecności!
                    </Typography>
                    <Typography variant="body1" sx={{ fontSize: '.8125rem', mt: 0.5 }}>
                        Zaloguj się aby kontynuować!
                    </Typography>
                </Box>
                <Box
                    sx={{
                        px: { xs: 6, md: 4 },
                        '& > form': { display: 'flex', flexDirection: 'column', gap: 4 },
                    }}>
                    <Formik
                        initialValues={{ email: '', password: '', error: null }}
                        onSubmit={async (values, { setErrors }) => {
                            try {
                                await accountStore.loginUser(values);
                            } catch {
                                setErrors({ error: 'Nieprawidłowy adres e-mail lub hasło!' });
                            }
                        }}>
                        {({ handleChange, handleBlur, errors }) => (
                            <Form>
                                <ErrorMessage
                                    name="error"
                                    render={() => (
                                        <Box
                                            sx={{
                                                backgroundColor: 'white',
                                                border: 1,
                                                borderColor: 'red',
                                                color: 'red',
                                                p: 2,
                                                borderRadius: '12px',
                                                mb: 1,
                                            }}>
                                            <Typography
                                                variant="subtitle1"
                                                component="strong"
                                                sx={{ fontWeight: 'bold' }}>
                                                Ups!
                                            </Typography>
                                            <Typography variant="body1" sx={{ ml: 1 }}>
                                                {errors.error}
                                            </Typography>
                                        </Box>
                                    )}
                                />
                                <TextField
                                    fullWidth
                                    id="email"
                                    name="email"
                                    label="Adres e-mail"
                                    variant="outlined"
                                    size="small"
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    required
                                    error={Boolean(errors.email)}
                                    helperText={errors.email}
                                    InputProps={{
                                        endAdornment: <InputAdornment position="end">@</InputAdornment>,
                                    }}
                                />
                                <TextField
                                    fullWidth
                                    id="password"
                                    name="password"
                                    label="Hasło"
                                    type="password"
                                    variant="outlined"
                                    size="small"
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    required
                                    error={Boolean(errors.password)}
                                    helperText={errors.password}
                                />
                                <Button
                                    type="submit"
                                    fullWidth
                                    variant="contained"
                                    size="medium"
                                    sx={{
                                        bgcolor: 'slate.700',
                                        '&:hover': { bgcolor: 'slate.500' },
                                        '&:focus': {
                                            ring: 4,
                                            ringColor: 'primary.300',
                                            outline: 'none',
                                        },
                                    }}>
                                    Zaloguj się
                                </Button>

                                <Box display="flex" justifyContent="center" marginBottom={1}>
                                    <Button variant="text" sx={{ fontSize: '0.875rem', color: 'primary.600' }}>
                                        Zapomniałeś hasła?
                                    </Button>
                                </Box>
                            </Form>
                        )}
                    </Formik>
                </Box>
            </Box>
            <Box display="flex" justifyContent="center" sx={{ mt: 2 }}>
                <Typography variant="body2" sx={{ color: 'gray.500' }}>
                    Nie masz jeszcze konta?{' '}
                    <a
                        href="#"
                        style={{
                            color: '#3B82F6',
                            textDecoration: 'underline',
                        }}>
                        Napisz do nas
                    </a>
                    !
                </Typography>
            </Box>
        </Container>
    );
});
