import { useStore } from '../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import {DataGrid, GridCellParams, GridColDef} from '@mui/x-data-grid';
import {Box, Button, Paper} from '@mui/material';
import HeaderBox from '../../app/components/shared/HeaderBox';
import { GRID_CONSTANTS } from '../../app/consts/gridConstants';
import CustomPagination from '../../app/components/shared/Controls/CustomPagination';
import {NavLink, useNavigate} from "react-router-dom";
import {URL_CONSTANTS} from "../../app/consts/urlConstants";

const columns: GridColDef[] = [
    {
        field: 'name',
        headerName: 'Nazwa',
        flex: 20,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
    },
    {
        field: 'teacher.name',
        headerName: 'Nauczyciel',
        flex: 20,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
        renderCell: (params: GridCellParams) => (
            <NavLink to={URL_CONSTANTS.TEACHERS_DETAILS(params.row.teacherId)}>{params.row.teacherName as string}</NavLink>
        ),
    },
    {
        field: 'schoolYearName',
        headerName: 'Rok szkolny',
        flex: 20,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
        renderCell: (params: GridCellParams) => (
            <NavLink to={URL_CONSTANTS.SCHOOL_YEARS_DETAILS(params.row.schoolYearId)}>{params.row.schoolYearName as string}</NavLink>
        ),
    },
    {
        field: 'id',
        headerName: 'Szczegóły',
        flex: 5,
        headerAlign: 'center',
        align: 'center',
        renderCell: (params: GridCellParams) => (
            <NavLink to={URL_CONSTANTS.GROUPS_DETAILS(params.row.id)}>Szczegóły</NavLink>
        ),
    }
];

function GroupList() {
    const { groupStore: store } = useStore();
    const navigate = useNavigate();
    
    useEffect(() => {
        (async () => {
            await store.loadGroups();
        })();

        return () => {
            store.abortController?.abort();
        };
    }, [store]);

    return (
        <Box>
            <HeaderBox title={'Lista grup'} />
            <Paper sx={{ mb: 2, p: 2 }}>
                <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                    <Button variant="contained" color="success" onClick={() => navigate('/groups/create')}>
                        Dodaj grupę
                    </Button>
                </Box>
            </Paper>
            {store.groups && (
                <Paper>
                    <DataGrid
                        initialState={{
                            pagination: {
                                paginationModel: store.pagination,
                            },
                        }}
                        autoHeight={true}
                        rows={store.groups.items}
                        columns={columns}
                        disableColumnFilter
                        disableColumnSelector
                        disableDensitySelector
                        pagination
                        paginationModel={store.pagination}
                        rowHeight={GRID_CONSTANTS.DEFAULT_ROW_HEIGHT}
                        onPaginationModelChange={store.handlePageChange}
                        pageSizeOptions={GRID_CONSTANTS.DEFAULT_PAGE_RANGE}
                        paginationMode="server"
                        sortingMode="server"
                        onSortModelChange={store.handleSortModelChange}
                        sortModel={store.sortModel}
                        hideFooterSelectedRowCount={true}
                        disableRowSelectionOnClick={true}
                        rowCount={store.groups.totalCount}
                        getRowId={(row) => row.id}
                        slots={{
                            pagination: (props) => <CustomPagination {...props} />,
                        }}
                    />
                </Paper>
            )}
        </Box>
    );
}

export default observer(GroupList);
