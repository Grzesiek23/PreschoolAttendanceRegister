import { Box, LinearProgress } from '@mui/material';
import { useStore } from '../../../stores/store';
import { observer } from 'mobx-react-lite';

export default observer(function Spinner() {
    const { commonStore } = useStore();
    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                mb: 1,
            }}>
            <Box sx={{ width: '100%', height: '4px' }}>{commonStore.loadingIndicator && <LinearProgress />}</Box>
        </Box>
    );
});
