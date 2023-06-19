import { Box, Button, Card, CardContent, Checkbox, FormControl, FormControlLabel, Typography } from '@mui/material';
import { Form, Formik } from 'formik';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { URL_CONSTANTS } from '../../app/consts/urlConstants';
import { useEffect, useState } from 'react';
import * as Yup from 'yup';
import { useStore } from '../../app/stores/store';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { SchoolYearFormValues } from '../../app/models/schoolYear';
import { useParams } from 'react-router';
import * as dayjs from 'dayjs';
import { observer } from 'mobx-react-lite';
import TextField from '@mui/material/TextField';

const validationSchema = Yup.object({});

function SchoolYearForm() {
    const { id } = useParams();
    const [schoolYear, setSchoolYear] = useState<SchoolYearFormValues>(new SchoolYearFormValues());
    const { schoolYearStore: store } = useStore();
    const navigate = useNavigate();

    useEffect(() => {
        (async () => {
            const numId = parseInt(id ?? '');
            if (numId) {
                await store.loadSchoolYear(numId);
                setSchoolYear(new SchoolYearFormValues(store.schoolYear));
            }
        })();
        return () => store.clearSchoolYear();
    }, [id, store]);

    const handleSubmit = async (values: SchoolYearFormValues): Promise<void> => {
        if (values.id) {
            const success = await store.updateSchoolYear(values);
            console.log(store.schoolYear);
            if (success) {
                navigate(URL_CONSTANTS.SCHOOL_YEARS_DETAILS(values.id));
                toast.success('Zaktualizowano rok szkolny');
            }
        } else {
            const success = await store.createSchoolYear(values);
            if (success) {
                navigate(URL_CONSTANTS.SCHOOL_YEARS_DETAILS(store.createdSchoolYearId));
                toast.success('Dodano nowy rok szkolny');
            }
        }
    };

    return (
        <Card>
            <Typography variant="h4" sx={{ ml: 2, mt: 2 }}>
                {schoolYear.id ? 'Edycja roku szkolnego' : 'Dodaj rok szkolny'}
            </Typography>
            <CardContent>
                <Formik
                    validationSchema={validationSchema}
                    initialValues={schoolYear}
                    onSubmit={handleSubmit}
                    enableReinitialize>
                    {({ values, handleChange, handleBlur, setFieldValue, touched, errors }) => (
                        <Form style={{ display: 'flex', flexDirection: 'column', gap: 15 }}>
                            <TextField
                                name="name"
                                label="Nazwa"
                                value={values.name}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                error={touched.name && Boolean(errors.name)}
                                helperText={touched.name && errors.name}
                                size="small"
                            />

                            <FormControl fullWidth={true}>
                                <DatePicker
                                    label="Początek"
                                    slotProps={{ textField: { size: 'small' } }}
                                    value={dayjs(values.startDate)}
                                    onChange={(value) => setFieldValue('startDate', value ?? new Date())}
                                />
                            </FormControl>

                            <FormControl fullWidth={true}>
                                <DatePicker
                                    label="Koniec"
                                    slotProps={{ textField: { size: 'small' } }}
                                    value={dayjs(values.endDate)}
                                    onChange={(value) => setFieldValue('endDate', value ?? new Date())}
                                />
                            </FormControl>

                            <FormControlLabel
                                control={
                                    <Checkbox
                                        name="isCurrent"
                                        value={values.isCurrent}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        checked={values.isCurrent}
                                    />
                                }
                                label="Obecny"
                            />

                            <Box mt={2}>
                                <Button type="submit" variant="contained" color="primary">
                                    {schoolYear.id ? 'Zapisz zmiany' : 'Utwórz rok szkolny'}
                                </Button>
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    sx={{ ml: 3 }}
                                    onClick={() => {
                                        store.clearSchoolYear();
                                        if (schoolYear.id) {
                                            navigate(URL_CONSTANTS.SCHOOL_YEARS_DETAILS(schoolYear.id));
                                        } else {
                                            navigate(URL_CONSTANTS.SCHOOL_YEARS);
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

export default observer(SchoolYearForm);
