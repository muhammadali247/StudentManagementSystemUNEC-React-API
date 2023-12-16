// import AdminFooter from "@/components/Admin/MainSections/Footer/AdminFooter";
import AdminSidebar from "../../components/Admin/MainSections/Sidebar/AdminSidebar";
import AdminTopbar from "../../components/Admin/MainSections/Topbar/AdminTopbar";
import styles from "./AdminLayout.module.scss";
import { Outlet } from "react-router-dom";

import { ColorModeContext, useMode } from "./AdminTheme";
import { CssBaseline, ThemeProvider } from "@mui/material";


function AdminLayout() {
  const [theme, colorMode] = useMode();

  return (
    <ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <div className={styles.root}>
          <div className={styles.sidebar}>
            <AdminSidebar />
          </div>
          <main className={styles.content}>
            <AdminTopbar />
            <div className={styles.rootContainer}>
              <Outlet />
            </div>
            {/* <AdminFooter /> */}
          </main>
        </div>
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
}

export default AdminLayout;
