import { useStore } from '../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import {DataGrid, GridCellParams, GridColDef} from '@mui/x-data-grid';
import {Box, Button, Checkbox, Paper} from '@mui/material';
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
        field: 'startDate',
        headerName: 'Początek',
        flex: 20,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
    },
    {
        field: 'endDate',
        headerName: 'Koniec',
        flex: 20,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
    },
    {
        field: 'isCurrent',
        headerName: 'Aktywny',
        flex: 5,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
        renderCell: (params: GridCellParams) => (
            <Checkbox checked={params.row.isCurrent as boolean} disabled={true} />
        )
    },
    {
        field: 'id',
        headerName: 'Szczegóły',
        flex: 5,
        headerAlign: 'center',
        align: 'center',
        renderCell: (params: GridCellParams) => (
            <NavLink to={URL_CONSTANTS.SCHOOL_YEARS_DETAILS(params.row.id)}>Szczegóły</NavLink>
        ),
    }
];

function SchoolYearList() {
    const { schoolYearStore: store } = useStore();
    const navigate = useNavigate();
    
    useEffect(() => {
        (async () => {
            await store.loadSchoolYears();
        })();

        return () => {
            store.abortController?.abort();
        };
    }, [store]);

    return (
        <Box>
            <HeaderBox title={'Lista roczników szkolnych'} />
            <Paper sx={{ mb: 2, p: 2 }}>
                <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                    <Button variant="contained" color="success" onClick={() => navigate('/school-years/create')}>
                        Dodaj rok szkolny
                    </Button>
                </Box>
            </Paper>
            {store.schoolYears && (
                <Paper>
                    <DataGrid
                        initialState={{
                            pagination: {
                                paginationModel: store.pagination,
                            },
                        }}
                        autoHeight={true}
                        rows={store.schoolYears.items}
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
                        rowCount={store.schoolYears.totalCount}
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

export default observer(SchoolYearList);
