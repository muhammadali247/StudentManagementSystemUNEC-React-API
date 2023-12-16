import { Box } from "@mui/material";
import Header from "../../../components/Admin/Header/Header";
import styles from "./Dashboard.module.scss";

function Dashboard() {
  return (
    <Box m="20px">
      HEADER
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Header
          title="DASHBOARD"
          subtitle="Welcome to your dashboard!"
        ></Header>
      </Box>
    </Box>
  );
}

export default Dashboard;
