import { Navigate, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";
import Dashboard from "../components/shared/Dashboard";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

function RequireAuth() {
  const {
    accountStore: { isLoggedIn },
  } = useStore();
  const location = useLocation();

  const [shouldRedirect, setShouldRedirect] = useState(false);

  useEffect(() => {
    if (!isLoggedIn()) {
      setShouldRedirect(true);
    }
  }, [isLoggedIn, location]);

  if (shouldRedirect) {
    return <Navigate to="/login" state={{ from: location }} />;
  }

  return <Dashboard />;
}

export default observer(RequireAuth);
