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
        field: 'firstName',
        headerName: 'Imię',
        flex: 20,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
    },
    {
        field: 'lastName',
        headerName: 'Nazwisko',
        flex: 20,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
    },
    {
        field: 'groupName',
        headerName: 'Grupa',
        flex: 20,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
        renderCell: (params: GridCellParams) => (
            <NavLink to={URL_CONSTANTS.GROUPS_DETAILS(params.row.groupId)}>{params.row.groupName as string}</NavLink>
        ),
    },
    {
        field: 'id',
        headerName: 'Szczegóły',
        flex: 5,
        headerAlign: 'center',
        align: 'center',
        renderCell: (params: GridCellParams) => (
            <NavLink to={URL_CONSTANTS.PRESCHOOLERS_DETAILS(params.row.id)}>Szczegóły</NavLink>
        ),
    }
];

function PreschoolerList() {
    const { preschoolerStore: store } = useStore();
    const navigate = useNavigate();
    
    useEffect(() => {
        (async () => {
            await store.loadPreschoolers();
        })();

        return () => {
            store.abortController?.abort();
        };
    }, [store]);

    return (
        <Box>
            <HeaderBox title={'Lista dzieci'} />
            <Paper sx={{ mb: 2, p: 2 }}>
                <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                    <Button variant="contained" color="success" onClick={() => navigate('/preschoolers/create')}>
                        Dodaj dziecko
                    </Button>
                </Box>
            </Paper>
            {store.preschoolers && (
                <Paper>
                    <DataGrid
                        initialState={{
                            pagination: {
                                paginationModel: store.pagination,
                            },
                        }}
                        autoHeight={true}
                        rows={store.preschoolers.items}
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
                        rowCount={store.preschoolers.totalCount}
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

export default observer(PreschoolerList);
