import Login from '../../../features/Login';
import { observer } from 'mobx-react-lite';
import {Container} from "@mui/material";

function AuthLayout() {
    return (
        <Container>
            <Login />
        </Container>
    );
}

export default observer(AuthLayout);