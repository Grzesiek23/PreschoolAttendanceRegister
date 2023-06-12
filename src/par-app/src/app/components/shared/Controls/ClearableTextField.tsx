import React from 'react';
import TextField from '@mui/material/TextField';
import InputAdornment from '@mui/material/InputAdornment';
import IconButton from '@mui/material/IconButton';
import { Clear } from '@mui/icons-material';
import { OutlinedTextFieldProps } from '@mui/material/TextField/TextField';

interface ClearableTextFieldProps extends Partial<OutlinedTextFieldProps> {
    onClearClick: () => void;
}

const ClearableTextField: React.FC<ClearableTextFieldProps> = ({ onClearClick, ...props }) => {
    const handleClearClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        onClearClick();
    };

    return (
        <TextField
            {...props}
            InputProps={{
                endAdornment: props.value !== undefined && props.value !== '' && (
                    <InputAdornment position="end">
                        <IconButton
                            aria-label="clear input field"
                            onMouseDown={(event) => event.preventDefault()}
                            onClick={handleClearClick}
                            edge="end">
                            <Clear fontSize="small" />
                        </IconButton>
                    </InputAdornment>
                ),
            }}
        />
    );
};
export default ClearableTextField;
