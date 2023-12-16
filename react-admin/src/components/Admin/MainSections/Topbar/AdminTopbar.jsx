import styles from "./AdminTopbar.module.scss";

import {
  Box,
  IconButton,
  useTheme,
  Menu,
  MenuItem,
  Typography,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { useContext, useState } from "react";
import { ColorModeContext, tokens } from "../../../../layouts/Admin/AdminTheme";
import InputBase from "@mui/material/InputBase";
import LightModeOutlinedIcon from "@mui/icons-material/LightModeOutlined";
import DarkModeOutlinedIcon from "@mui/icons-material/DarkModeOutlined";
import NotificationsOutlinedIcon from "@mui/icons-material/NotificationsOutlined";
import SettingsOutlinedIcon from "@mui/icons-material/SettingsOutlined";
import LibraryMusicIcon from '@mui/icons-material/LibraryMusic';
import PersonOutlinedIcon from "@mui/icons-material/PersonOutlined";
import SearchIcon from "@mui/icons-material/Search";
import LogoutIcon from "@mui/icons-material/Logout";
import Logout from "../../../../components/Auth/Logout/Logout";
import useAuth from "../../../../utils/useAuth";
import LoginIcon from "@mui/icons-material/Login";
import AppRegistrationIcon from "@mui/icons-material/AppRegistration";
import authRoutes from "../../../../routing/authRoutes";
// import clientRoutes from "@/routing/clientRoutes";
import { findPathByLabel } from "../../../../utils/routingUtils";
import { useNavigate } from "react-router-dom";

function AdminTopbar() {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const colorMode = useContext(ColorModeContext);
  const { user } = useAuth();
  const [anchorEl, setAnchorEl] = useState(null);
  const navigate = useNavigate();

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <Box display="flex" justifyContent="space-between" p={2}>
      <Box
        display="flex"
        backgroundColor={colors.primary[400]}
        borderRadius="3px"
      >
        <InputBase sx={{ ml: 2, flex: 1 }} placeholder="Search" />
        <IconButton type="button" sx={{ p: 1 }}>
          <SearchIcon />
        </IconButton>
      </Box>
      <Box display="flex">
        <IconButton onClick={colorMode.toggleColorMode}>
          {theme.palette.mode === "dark" ? (
            <DarkModeOutlinedIcon />
          ) : (
            <LightModeOutlinedIcon />
          )}
        </IconButton>
        <IconButton>
          <NotificationsOutlinedIcon />
        </IconButton>
        {/* <IconButton onClick={() => navigate(findPathByLabel(clientRoutes, "Home"))}> */}
        <IconButton>
          <LibraryMusicIcon />
        </IconButton>
        <IconButton onClick={handleClick}>
          <PersonOutlinedIcon />
        </IconButton>

        <Menu
          anchorEl={anchorEl}
          keepMounted
          open={Boolean(anchorEl)}
          onClose={handleClose}
          PaperProps={{
            sx: {
              backgroundColor: colors.blueAccent[700],
              borderRadius: "8px",
              boxShadow: "0px 3px 10px rgba(0, 0, 0, 0.2)",
            },
          }}
        >
          {user ? (
            <Box>
              <MenuItem
                onClick={() => {
                  handleClose();
                  // navigate(findPathByLabel(clientRoutes, "Account"));
                }}
              >
                <ListItemIcon>
                  <PersonOutlinedIcon fontSize="small" />
                </ListItemIcon>
                <ListItemText
                  primary={
                    <Typography fontWeight="bold" fontSize="1rem">
                      My Account
                    </Typography>
                  }
                />
              </MenuItem>
              <MenuItem onClick={handleClose}>
                <ListItemIcon>
                  <LogoutIcon fontSize="small" />
                </ListItemIcon>
                <Logout>
                  <Typography
                    fontWeight="bold"
                    fontSize="1rem"
                    color={theme.palette.mode === "dark" ? "white" : "black"}
                  >
                    Logout
                  </Typography>{" "}
                </Logout>
              </MenuItem>
            </Box>
          ) : (
            <Box>
              <MenuItem
                onClick={() => {
                  handleClose();
                  navigate(findPathByLabel(authRoutes, "SignIn"));
                }}
              >
                <ListItemIcon>
                  <LoginIcon fontSize="small" />
                </ListItemIcon>
                <ListItemText
                  primary={
                    <Typography fontWeight="bold" fontSize="1rem">
                      SignIn
                    </Typography>
                  }
                />
              </MenuItem>
              <MenuItem
                onClick={() => {
                  handleClose();
                  navigate(findPathByLabel(authRoutes, "SignUp"));
                }}
              >
                <ListItemIcon>
                  <AppRegistrationIcon fontSize="small" />
                </ListItemIcon>
                <ListItemText
                  primary={
                    <Typography fontWeight="bold" fontSize="1rem">
                      SignUp
                    </Typography>
                  }
                />
              </MenuItem>
            </Box>
          )}
        </Menu>
      </Box>
    </Box>
  );
}

export default AdminTopbar;
