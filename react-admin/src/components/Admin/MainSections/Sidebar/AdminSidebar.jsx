import { useState, useEffect } from "react";
import { ProSidebar, Menu, MenuItem, SubMenu } from "react-pro-sidebar";
import { Box, IconButton, Typography, useTheme } from "@mui/material";
import { NavLink } from "react-router-dom";
import adminRoutes from "../../../../routing/adminRoutes";
import { findPathByLabel } from "../../../../utils/routingUtils";
import "react-pro-sidebar/dist/css/styles.css"; //deprecated
import { ColorModeContext, tokens } from "../../../../layouts/Admin/AdminTheme";
import DashboardIcon from "@mui/icons-material/Dashboard"; //dashboard
import AlbumIcon from "@mui/icons-material/Album"; // album
import BeenhereIcon from "@mui/icons-material/Beenhere"; // role
import PersonPinIcon from "@mui/icons-material/PersonPin"; // user
import StorageIcon from "@mui/icons-material/Storage"; // data (entities)
import SpatialAudioIcon from "@mui/icons-material/SpatialAudio"; // artist
import GroupWorkIcon from "@mui/icons-material/GroupWork"; //musicalProject
import UserIcon from "../../../../assets/icons/user.png";
import MenuOutlinedIcon from "@mui/icons-material/MenuOutlined";
import EventIcon from "@mui/icons-material/Event"; // events
import BookOnlineIcon from "@mui/icons-material/BookOnline"; // tickets
import MusicNoteIcon from "@mui/icons-material/MusicNote"; // songs
import useAuth from "../../../../utils/useAuth";
import styles from "./AdminSidebar.module.scss";
import axiosInstance from "../../../../utils/axiosInstance";
// Replace 'ProSidebar' with the correct component name
// import {
//   USERS_GET_BY_ID_LIMITED_URL,
//   BASE_WEB_URL,
// } from "@/constants/Urls";
import { BASE_WEB_URL, USERS_GET_BY_ID_LIMITED_URL } from "../../../../constants/URLs"
import useService from "../../../../hooks";
import { tokenRoleProperty } from "../../../Auth/AuthProvider/AuthProvider";
import { jwtDecode } from 'jwt-decode';

const Item = ({ title, to, icon, selected, setSelected }) => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  return (
    <MenuItem
      active={selected === title}
      style={{
        color: colors.grey[100],
      }}
      onClick={() => setSelected(title)}
      icon={icon}
    >
      <Typography variant="h5">{title}</Typography>
      <NavLink to={to} />
    </MenuItem>
  );
};

function AdminSidebar() {
  // Get user data from the auth hook
  const { user, updateUser } = useAuth();
  // const [profileImage, setProfileImage] = useState(UserIcon);
  const [profileImage, setProfileImage] = useState(null);

  const theme = useTheme();
  const colors = tokens(theme.palette.mode);

  const isScreenMediumOrSmaller = () => window.innerWidth <= 960;

  const [isCollapsed, setIsCollapsed] = useState(isScreenMediumOrSmaller());
  const [selected, setSelected] = useState("Dashboard");
  const { userServices } = useService();

  // useEffect(() => {
  //   if (user) {

  //     axiosInstance.get(USERS_GET_BY_ID_LIMITED_URL(user.id))
  //       .then(response => {
  //         console.log(response);
  //         // Assuming the URL of the profile image is returned in the response
  //         setProfileImage(BASE_WEB_URL + response.data.profileImageUrl);
  //       })
  //       .catch(error => {
  //         console.error('Failed to load profile image:', error);
  //       });
  //   }
  // }, [user]);


  useEffect(() => {
    async function fetchData() {
      try {
        if (user) {
          console.log(user);
          const response = await userServices.getUserById(user.id);
          console.log(response);
          if (response && response.data.image) {
            // localStorage.setItem("accessToken", response.data.accessToken);
            // const decodedToken = jwtDecode(response.data.accessToken);
            // updateUser(decodedToken);
            // const expirationTime = decodedToken.exp;
            // localStorage.setItem("tokenExpiration", expirationTime);
              
            switch (user.roles) {
              case "Teacher":
              setProfileImage(`${BASE_WEB_URL}/assets/uploads/images/teacher-images/${response.data.image}`);
              break;
            case "Student":
              setProfileImage(`${BASE_WEB_URL}/assets/uploads/images/student-images/${response.data.image}`);
              break;
            default:
              // console.log("Unknown role:", decodedToken[tokenRoleProperty]);
              setProfileImage(UserIcon);
            }
            // Construct the URL by appending the image name to your BASE_WEB_URL
            setProfileImage(`${BASE_WEB_URL}/assets/uploads/images/student-images/${response.data.image}`);
          } else {
            console.error('Profile image URL not found in the response.');
          }
        } else {
          console.error('User is null. Cannot fetch profile image.');
        }
      } catch (error) {
        console.error('Failed to load profile image:', error);
      }
    }

    fetchData();
  }, [user]);


  useEffect(() => {
    const handleResize = () => {
      if (isScreenMediumOrSmaller()) {
        setIsCollapsed(true);
      } else {
        setIsCollapsed(false);
      }
    };

    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, []);

  return (
    <Box
      sx={{
        "& .pro-sidebar-inner": {
          background: `${colors.primary[400]} !important`,
          height: "100vh", // sets the maximum height to the height of the viewport
          overflowY: "auto", // makes it scrollable when content overflows
        },
        "& .pro-icon-wrapper": {
          backgroundColor: "transparent !important",
        },
        "& .pro-inner-item": {
          padding: "5px 35px 5px 20px !important",
        },
        "& .pro-inner-item:hover": {
          color: "#868dfb !important",
        },
        "& .pro-menu-item.active": {
          color: "#6870fa !important",
        },
      }}
    >
      <ProSidebar
        collapsed={isCollapsed}
        className={styles.proSidebarCollapsed}
      >
        <Menu iconShape="square">
          {/* LOGO AND MENU ICON */}
          <MenuItem
            onClick={() => setIsCollapsed(!isCollapsed)}
            icon={isCollapsed ? <MenuOutlinedIcon /> : undefined}
            style={{
              margin: "10px 0 10px 0",
              color: colors.grey[100],
            }}
          >
            {!isCollapsed && (
              <Box
                display="flex"
                justifyContent="space-between"
                alignItems="center"
                ml="15px"
              >
                <Typography
                  variant="h3"
                  color={colors.grey[100]}
                  style={{
                    fontWeight: "bold",
                  }}
                >
                  ADMIN PANEL
                </Typography>
                <IconButton onClick={() => setIsCollapsed(!isCollapsed)}>
                  <MenuOutlinedIcon />
                </IconButton>
              </Box>
            )}
          </MenuItem>

          {!isCollapsed && (
            <Box mb="25px">
              <Box display="flex" justifyContent="center" alignItems="center">
                {/* <img
                  alt="profile-user"
                  width="70px"
                  height="70px"
                  src={profileImage ? profileImage : UserIcon}
                  style={{ cursor: "pointer", borderRadius: "50%" }}
                /> */}
                <img
                  alt="profile-user"
                  width="100px"
                  height="100px"
                  src={profileImage || UserIcon}
                  style={{ cursor: "pointer", borderRadius: "50%" }}
                />
              </Box>
              <Box textAlign="center">
                {/* <Typography
                  variant="h3"
                  color={colors.grey[100]}
                  fontWeight="bold"
                  sx={{ m: "10px 0 0 0" }}
                >
                  {user
                    ? `${user.firstname} ${user.lastname}`
                    : "Unknown Unknown"}
                </Typography> */}
                <Typography variant="h3" color={colors.greenAccent[500]} sx={{marginTop: "15px"}}>
                  {user ? user.roles : "Unknown Role"}
                </Typography>
              </Box>
            </Box>
          )}

          <Box paddingLeft={isCollapsed ? undefined : "10%"}>
            <Item
              title="Dashboard"
              to={findPathByLabel(adminRoutes, "Dashboard")}
              icon={<DashboardIcon />}
              selected={selected}
              setSelected={setSelected}
            />

            <SubMenu
              title={
                <Typography
                  variant="h5"
                  color={colors.grey[300]}
                  sx={{ m: "5px 0 5px 20px" }}
                >
                  Data
                </Typography>
              }
              icon={<StorageIcon />}
            >
              <Item
                title="Students"
                to={findPathByLabel(adminRoutes, "Students")}
                icon={<SpatialAudioIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              <Item
                title="Groups"
                to={findPathByLabel(adminRoutes, "Groups")}
                icon={<GroupWorkIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              <Item
                title="GroupSubjects"
                to={findPathByLabel(adminRoutes, "GroupSubjects")}
                icon={<AlbumIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              <Item
                title="Subjects"
                to={findPathByLabel(adminRoutes, "Subjects")}
                icon={<MusicNoteIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              <Item
                title="Lesson types"
                to={findPathByLabel(adminRoutes, "LessonTypes")}
                icon={<BookOnlineIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              <Item
                title="TeacherSubjects"
                to={findPathByLabel(adminRoutes, "TeacherSubjects")}
                icon={<EventIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              {/* <Item
                title="Teachers"
                to={findPathByLabel(adminRoutes, "Teachers")}
                icon={<BookOnlineIcon />}
                selected={selected}
                setSelected={setSelected}
              /> */}
              <Item
                title="Teachers"
                to={findPathByLabel(adminRoutes, "Teachers")}
                icon={<BookOnlineIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              <Item
                title="Teacher roles"
                to={findPathByLabel(adminRoutes, "TeacherRoles")}
                icon={<BookOnlineIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              <Item
                title="Faculties"
                to={findPathByLabel(adminRoutes, "Faculties")}
                icon={<BookOnlineIcon />}
                selected={selected}
                setSelected={setSelected}
              />
              <Item
                title="Exam types"
                to={findPathByLabel(adminRoutes, "ExamTypes")}
                icon={<BookOnlineIcon />}
                selected={selected}
                setSelected={setSelected}
              />
            </SubMenu>

            <Typography
              variant="h5"
              color={colors.grey[300]}
              sx={{ m: "15px 0 5px 20px" }}
            >
              Identity
            </Typography>
            <Item
              title="Users"
              to={findPathByLabel(adminRoutes, "Users")}
              icon={<PersonPinIcon />}
              selected={selected}
              setSelected={setSelected}
            />
            {/* <Item
              title="Roles"
              to={findPathByLabel(adminRoutes, "Roles")}
              icon={<BeenhereIcon />}
              selected={selected}
              setSelected={setSelected}
            /> */}
          </Box>
        </Menu>
      </ProSidebar>
    </Box>
  );
}

export default AdminSidebar;
