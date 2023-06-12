import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@mui/material';

interface Props {
    title?: string;
    message: string;
    open: boolean;
    setOpen: (open: boolean) => void;
    handleDelete: () => void;
}

export default function ConfirmationDialog({ open, setOpen, handleDelete, message, title }: Props) {
    return (
        <Dialog
            fullWidth={true}
            open={open}
            onClose={() => setOpen(false)}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description">
            <DialogTitle id="alert-dialog-title">{title ?? 'Potwierdzenie akcji'}</DialogTitle>
            <DialogContent>
                <DialogContentText id="alert-dialog-description">{message}</DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => setOpen(false)}>Anuluj</Button>
                <Button onClick={handleDelete} autoFocus>
                    Usuń
                </Button>
            </DialogActions>
        </Dialog>
    );
}