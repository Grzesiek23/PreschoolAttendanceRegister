import {useStore} from "./app/stores/store";
import {useEffect} from "react";
import Spinner from "./app/components/shared/Spinner";
import {ToastContainer} from "react-toastify";
import Login from "./features/Login";
import {Outlet} from "react-router";
import {observer} from "mobx-react-lite";

function App() {
    const { commonStore, accountStore } = useStore();

    useEffect(() => {
        commonStore.setAppLoaded();
    }, [commonStore, accountStore]);

    if (!commonStore.appLoaded) return <Spinner />;

    return (
        <>
            <ToastContainer position="top-right" hideProgressBar theme="colored" autoClose={1250} />
            {!accountStore.isLoggedIn ? <Login /> : <Outlet />}
        </>
    );
}

export default observer(App);
