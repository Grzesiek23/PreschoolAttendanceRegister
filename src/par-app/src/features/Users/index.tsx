import { useStore } from '../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import { DataGrid, GridCellParams, GridColDef } from '@mui/x-data-grid';
import { NavLink } from 'react-router-dom';
import { URL_CONSTANTS } from '../../app/consts/urlConstants';
import { Box, Paper } from '@mui/material';
import HeaderBox from '../../app/components/shared/HeaderBox';
import { GRID_CONSTANTS } from '../../app/consts/gridConstants';
import CustomPagination from '../../app/components/shared/Controls/CustomPagination';
import Filters from './Components/Filters';

const columns: GridColDef[] = [
    {
        field: 'fullName',
        headerName: 'Użytkownik',
        width: 200,
        renderCell: (params: GridCellParams) => (
            <NavLink to={URL_CONSTANTS.USERS_DETAILS(params.row.id)}>{params.row.fullName as string}</NavLink>
        ),
    },
    {
        field: 'email',
        headerName: 'Adres e-mail',
        width: 250,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
    },
    {
        field: 'phoneNumber',
        headerName: 'Telefon',
        width: 180,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
        valueGetter: (params: GridCellParams) => params.row.phoneNumber || 'brak',
    },
    {
        field: 'role',
        headerName: 'Rola',
        width: 180,
        headerAlign: 'center',
        align: 'center',
        disableColumnMenu: true,
        sortable: false,
        valueGetter: (params: GridCellParams) => `${params.row.role.name}`,
    },
];

function UsersList() {
    const { userStore: store } = useStore();

    useEffect(() => {
        (async () => {
            await store.loadUsers();
        })();

        return () => {
            store.abortController?.abort();
        };
    }, [store]);

    return (
        <Box>
            <HeaderBox title={'Lista użytkowników'} />
            <Paper sx={{ mb: 2, p: 2 }}>
                <Filters />
            </Paper>
            {store.users && (
                <Paper>
                    <DataGrid
                        initialState={{
                            pagination: {
                                paginationModel: store.pagination,
                            },
                        }}
                        autoHeight={true}
                        rows={store.users.items}
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
                        rowCount={store.users.totalCount}
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

export default observer(UsersList);
