import styles from "./AuthLayout.module.scss";
import { Outlet } from "react-router-dom";
import { CssBaseline, ThemeProvider } from "@mui/material";
import { createTheme } from "@mui/material/styles";


const theme = createTheme({
    palette: {
      mode: 'dark',
      background: {
        default: "#1F2A40",  // setting default background color
      },
    },
  });

function AuthLayout() {
  return (
    <ThemeProvider theme={theme}>
        <CssBaseline />
        <div className={styles.root}>
          <main className={styles.content}>
            <div className={styles.rootContainer}>
              <Outlet />
            </div>
          </main>
        </div>
    </ThemeProvider>
  );
}

export default AuthLayout;