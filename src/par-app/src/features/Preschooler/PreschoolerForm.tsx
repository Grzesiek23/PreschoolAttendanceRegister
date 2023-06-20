import {
    Box,
    Button,
    Card,
    CardContent,
    FormControl,
    FormHelperText, InputLabel,
    MenuItem, Select,
    Typography
} from '@mui/material';
import { Form, Formik } from 'formik';
import { URL_CONSTANTS } from '../../app/consts/urlConstants';
import  { useEffect, useState } from 'react';
import * as Yup from 'yup';
import { useStore } from '../../app/stores/store';
import { useNavigate } from 'react-router-dom';
import { useParams } from 'react-router';
import { observer } from 'mobx-react-lite';
import TextField from '@mui/material/TextField';
import {toast} from "react-toastify";
import {PreschoolerFormValues} from "../../app/models/preschooler";

const validationSchema = Yup.object({});

function PreschoolerForm() {
    const { id } = useParams();
    const [preschooler, setPreschooler] = useState<PreschoolerFormValues>(new PreschoolerFormValues());
    const { preschoolerStore: store, groupStore } = useStore();
    const navigate = useNavigate();

    useEffect(() => {
        (async () => {
            await groupStore.loadGroupsOptionList();
            const numId = parseInt(id ?? '');
            if (numId) {
                await store.loadPreschooler(numId);
                setPreschooler(new PreschoolerFormValues(store.preschooler));
            }
        })();
        return () => store.clearPreschooler();
    }, [id, store, groupStore]);

    const handleSubmit = async (values: PreschoolerFormValues): Promise<void> => {
        if (values.id) {
            const success = await store.updatePreschooler(values);
            console.log(store.preschooler);
            if (success) {
                navigate(URL_CONSTANTS.PRESCHOOLERS_DETAILS(values.id));
                toast.success('Zaktualizowano dziecko');
            }
        } else {
            const success = await store.createPreschooler(values);
            if (success) {
                navigate(URL_CONSTANTS.PRESCHOOLERS_DETAILS(store.createdPreschoolerId));
                toast.success('Dodano nową grupę');
            }
        }
    };

    return (
        <Card>
            <Typography variant="h4" sx={{ ml: 2, mt: 2 }}>
                {preschooler.id ? 'Edycja dziecka' : 'Dodaj dziecko'}
            </Typography>
            <CardContent>
                <Formik
                    validationSchema={validationSchema}
                    initialValues={preschooler}
                    onSubmit={handleSubmit}
                    enableReinitialize>
                    {({ values, handleChange, handleBlur, setFieldValue, touched, errors }) => (
                        <Form style={{ display: 'flex', flexDirection: 'column', gap: 15 }}>
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
                            
                            <FormControl>
                                <InputLabel id="groupIdLabel">Grupa</InputLabel>
                                <Select
                                    name="groupId"
                                    labelId="groupIdLabel"
                                    label="Grupa"
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    value={values.groupId}
                                    error={touched.groupId && Boolean(errors.groupId)}
                                    size="small"
                                    MenuProps={{
                                        anchorOrigin: { vertical: 'bottom', horizontal: 'left' },
                                        transformOrigin: { vertical: 'top', horizontal: 'left' },
                                        PaperProps: {
                                            style: {
                                                maxHeight: 300, // Set the max height of the menu
                                                overflow: 'auto', // Add scroll functionality when the menu exceeds max height
                                            },
                                        },
                                    }}>
                                    <MenuItem value={0}>Wybierz</MenuItem>
                                    {groupStore.groupListOption.map((name) => (
                                        <MenuItem key={name.id} value={name.id}>
                                            {name.name}
                                        </MenuItem>
                                    ))}
                                </Select>
                                {touched.groupId && Boolean(errors.groupId) && (
                                    <FormHelperText sx={{ color: '#d32f2f', ml: 2 }}>{errors.groupId}</FormHelperText>
                                )}
                            </FormControl>

                            <Box mt={2}>
                                <Button type="submit" variant="contained" color="primary">
                                    {preschooler.id ? 'Zapisz zmiany' : 'Utwórz dziecko'}
                                </Button>
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    sx={{ ml: 3 }}
                                    onClick={() => {
                                        store.clearPreschooler();
                                        if (preschooler.id) {
                                            navigate(URL_CONSTANTS.PRESCHOOLERS_DETAILS(preschooler.id));
                                        } else {
                                            navigate(URL_CONSTANTS.PRESCHOOLERS);
                                        }
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

export default observer(PreschoolerForm);
