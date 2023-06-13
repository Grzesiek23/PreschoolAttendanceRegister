import { Navigate, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";
import Dashboard from "../components/shared/Dashboard";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

function RequireAuth() {
  const {
    accountStore: { user, isLoggedIn },
  } = useStore();

  const [shouldRedirect, setShouldRedirect] = useState(!isLoggedIn());

  useEffect(() => {
    setShouldRedirect(!isLoggedIn());
  }, [user]);

  if (shouldRedirect) {
    const location = useLocation();
    return <Navigate to="/login" state={{ from: location }} />;
  }

  return <Dashboard />;
}

export default observer(RequireAuth);