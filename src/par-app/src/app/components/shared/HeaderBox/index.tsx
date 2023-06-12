import {Box, Typography} from "@mui/material";

type Props = {
    title: string;
};

export default function HeaderBox(props: Props) {
    return (
        <Box sx={{
            display: 'flex',
            backgroundColor: 'white',
            p: 1.5,
            mb: 2,
            borderRadius: 2,
            border: '1px solid #e0e0e0',
        }}>
            <Typography variant="h4">
                {props.title}
            </Typography>
        </Box>
    );
}
