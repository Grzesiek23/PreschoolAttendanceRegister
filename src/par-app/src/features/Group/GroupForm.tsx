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
import {GroupFormValues} from "../../app/models/group";
import {toast} from "react-toastify";

const validationSchema = Yup.object({});

function GroupForm() {
    const { id } = useParams();
    const [group, setGroup] = useState<GroupFormValues>(new GroupFormValues());
    const { groupStore: store, schoolYearStore, userStore } = useStore();
    const navigate = useNavigate();

    useEffect(() => {
        (async () => {
            await schoolYearStore.loadSchoolYearsOptionList();
            await userStore.loadUsersOptionList();
            const numId = parseInt(id ?? '');
            if (numId) {
                await store.loadGroup(numId);
                setGroup(new GroupFormValues(store.group));
            }
        })();
        return () => store.clearGroup();
    }, [id, store, userStore, schoolYearStore]);

    const handleSubmit = async (values: GroupFormValues): Promise<void> => {
        if (values.id) {
            const success = await store.updateGroup(values);
            console.log(store.group);
            if (success) {
                navigate(URL_CONSTANTS.GROUPS_DETAILS(values.id));
                toast.success('Zaktualizowano grupę');
            }
        } else {
            const success = await store.createGroup(values);
            if (success) {
                navigate(URL_CONSTANTS.GROUPS_DETAILS(store.createdGroupId));
                toast.success('Dodano nową grupę');
            }
        }
    };

    return (
        <Card>
            <Typography variant="h4" sx={{ ml: 2, mt: 2 }}>
                {group.id ? 'Edycja grupy' : 'Dodaj grupę'}
            </Typography>
            <CardContent>
                <Formik
                    validationSchema={validationSchema}
                    initialValues={group}
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

                            <FormControl>
                                <InputLabel id="schoolYearIdLabel">Rok szkolny</InputLabel>
                                <Select
                                    name="schoolYearId"
                                    labelId="schoolYearIdLabel"
                                    label="Rok szkolny"
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    value={values.schoolYearId}
                                    error={touched.schoolYearId && Boolean(errors.schoolYearId)}
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
                                    {schoolYearStore.schoolYearListOption.map((name) => (
                                        <MenuItem key={name.id} value={name.id}>
                                            {name.name}
                                        </MenuItem>
                                    ))}
                                </Select>
                                {touched.schoolYearId && Boolean(errors.schoolYearId) && (
                                    <FormHelperText sx={{ color: '#d32f2f', ml: 2 }}>{errors.schoolYearId}</FormHelperText>
                                )}
                            </FormControl>

                            <FormControl>
                                <InputLabel id="teacherIdLabel">Nauczyciel</InputLabel>
                                <Select
                                    name="teacherId"
                                    labelId="teacherIdLabel"
                                    label="Nauczyciel"
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    value={values.teacherId}
                                    error={touched.teacherId && Boolean(errors.teacherId)}
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
                                    {userStore.userOptionList.map((name) => (
                                        <MenuItem key={name.id} value={name.id}>
                                            {name.name}
                                        </MenuItem>
                                    ))}
                                </Select>
                                {touched.teacherId && Boolean(errors.teacherId) && (
                                    <FormHelperText sx={{ color: '#d32f2f', ml: 2 }}>{errors.teacherId}</FormHelperText>
                                )}
                            </FormControl>
                           

                            <Box mt={2}>
                                <Button type="submit" variant="contained" color="primary">
                                    {group.id ? 'Zapisz zmiany' : 'Utwórz grupę'}
                                </Button>
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    sx={{ ml: 3 }}
                                    onClick={() => {
                                        store.clearGroup();
                                        if (group.id) {
                                            navigate(URL_CONSTANTS.GROUPS_DETAILS(group.id));
                                        } else {
                                            navigate(URL_CONSTANTS.GROUPS);
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

export default observer(GroupForm);
