// import React, { useState, useEffect } from "react";
// import { useParams, useNavigate } from "react-router-dom";
// import { Box, Button, Typography, Paper, List, ListItem, useTheme } from "@mui/material";
// import axiosInstance from "../../../utils/axiosInstance";
// import useService from "../../../hooks";
// import adminRoutes from "../../../routing/adminRoutes";
// import { findPathByLabel } from "../../../utils/routingUtils";
// import Header from "../../../components/Admin/Header/Header";



// import { DataGrid, GridToolbar } from "@mui/x-data-grid";
// import { tokens } from "../../../layouts/Admin/AdminTheme";
// import IconButton from "@mui/material/IconButton";
// import DeleteIcon from "@mui/icons-material/Delete";
// import EditIcon from "@mui/icons-material/Edit";
// import ViewIcon from "@mui/icons-material/Visibility";
// import { darken } from "@mui/system";
// import Tooltip from "../../../components/Global/UI/Tooltip";
// import LinearProgress from "@mui/material/LinearProgress";

// function UserDetailsForm() {
//   const { userId } = useParams();
//   const navigate = useNavigate();
//   const { userServices } = useService();

//   const [userData, setUserData] = useState(null);
// //   const [facGroupData, setFacGroupData] = useState(null);

//   const theme = useTheme();
//   const colors = tokens(theme.palette.mode);
//   const [loading, setLoading] = useState(true);
  

//   useEffect(() => {
//     async function fetchData() {
//       try {
//         var response = await userServices.getUserById(userId);
//         setUserData(response.data);
//         // setFacGroupData(response.data.groups);
//       } catch (error) {
//         console.error("Error fetching user details:", error);
//       }
//     }

//     fetchData();
//   }, [userId]);

//   if (!userData) {
//     return <div>Loading...</div>;
//   }

//   const columns = [
//     { field: "id", headerName: "ID", flex: 0.3 },
//     { field: "image", headerName: "Image Url", flex: 0.15 },
//     { field: "userName", headerName: "User Name", flex: 0.17 },
//     { field: "emailConfirmed", headerName: "Email Confirmed", flex: 0.25 },
//     { field: "studentName", headerName: "Student Name", flex: 0.15 },
//     { field: "teacherName", headerName: "Teacher Name", flex: 0.15 },
//   ];

//   return (
//     <Box m="20px">
//       <Paper elevation={3} sx={{ padding: "20px", backgroundColor: "#1f2a40" }}>
//         {/* <Typography variant="h4" gutterBottom>
//           {facultyData.name}
//         </Typography>
//         <Typography variant="h6" color="textSecondary" gutterBottom>
//           Groups:
//         </Typography>
//         <List>
//           {facultyData.groupNames.map((groupName, index) => (
//             <ListItem key={index}>
//               {groupName}
//             </ListItem>
//           ))}
//         </List> */}
//         <Header title={`${userData.userName}`} subtitle="The list of users!"/>
//         <Box
//         m="0 0 0 0"
//         height="520px"
//         sx={{
//           "& .MuiDataGrid-root": {},
//           "& .MuiDataGrid-cell": {
//             fontSize: "0.9rem",
//             borderRight: "2px solid rgba(0, 0, 0, 0.1)",
//           },
//           "& .MuiDataGrid-columnHeaders": {
//             backgroundColor: colors.blueAccent[700],
//             fontSize: "1rem",
//           },
//           "& .MuiDataGrid-columnHeader": {
//             borderRight: "2px solid #003366",
//           },
//           "& .MuiDataGrid-columnHeaderTitle": {
//             fontWeight: "bold",
//           },
//           "& .MuiDataGrid-virtualScroller": {
//             backgroundColor: colors.primary[400],
//           },
//           "& .MuiDataGrid-footerContainer": {
//             backgroundColor: colors.blueAccent[700],
//             // display: "none"
//           },
//           "& .MuiDataGrid-toolbarContainer": {
//             "& .MuiButton-root, & .MuiIconButton-root, & .MuiTypography-root": {
//               color: colors.grey[100],
//             },
//           },
//         }}
//       >
//         <DataGrid
//           loading={loading}
//           slots={{
//             toolbar: GridToolbar,
//             loadingOverlay: LinearProgress,
//           }}
//           slotProps={{
//             toolbar: {
//               showQuickFilter: true,
//               quickFilterProps: { debounceMs: 500 },
//             },
//           }}
//           //   rows={facGroupData}
//           rows={userData}
//           columns={columns}
//           initialState={{
//             pagination: {
//               paginationModel: { pageSize: 10, page: 0 },
//             },
//           }}
//           pageSizeOptions={[5, 10, 25, 100]}
//         />
//       </Box>
//         {/* <Button
//           variant="outlined"
//           color="secondary"
//           sx={{marginTop: "10px"}}
//           onClick={() => {
//             // Navigate to an edit page or open a modal/dialog for editing
//             navigate(findPathByLabel(adminRoutes, "FacultiesUpdate", { facultyId: facultyId }));
//           }}
//         >
//           Edit
//         </Button> */}

//         <Button
//           variant="outlined"
//           color="secondary"
//           sx={{
//             marginLeft: "10px",
//             marginTop: "10px",
//             color: "#2196f3",
//             border: "1px solid #2196f3",
//             "&:hover": {
//               color: "#3d5afe",
//               backgroundColor: "transparent",
//               border: "1px solid #2196f3",
//             },
//           }}
//           onClick={() => {
//             // Navigate to an edit page or open a modal/dialog for editing
//             navigate(findPathByLabel(adminRoutes, "Users"));
//           }}
//         >
//           Back to List
//         </Button>
//       </Paper>
//     </Box>
//   );
// }

// export default UserDetailsForm;






















import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  Box,
  Typography,
  Paper,
  Card,
  CardContent,
  Avatar,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Button,
  useTheme, 
  Chip 
} from "@mui/material";
import axiosInstance from "../../../utils/axiosInstance";
// import { USERS_GET_BY_ID_LIMITED_URL, BASE_WEB_URL} from "@/constants/Urls";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import useService from "../../../hooks";
import { BASE_WEB_URL } from "../../../constants/URLs";
import adminRoutes from "../../../routing/adminRoutes";
import { findPathByLabel } from "../../../utils/routingUtils";

const InfoPair = ({ label, value }) => {
  return (
    <Box display="flex" alignItems="center" marginBottom={1}>
      <Typography variant="h5" style={{ minWidth: '120px', fontWeight: 'bold' }}>{label}</Typography>
      <Typography variant="h5" style={{ fontWeight: 'normal' }}>{value}</Typography>
    </Box>
  );
};

export default function UsersDetailsForm() {
  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const { userId } = useParams();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const navigate = useNavigate();
  const { userServices } = useService();

  useEffect(() => {
    async function fetchData() {
      try {
        // var response = await userServices.getUserById(userId);
        // setUserData(response.data);
        // // setFacGroupData(response.data.groups);

        setLoading(true);
        // const url = USERS_GET_BY_ID_LIMITED_URL(userId);
        // console.log("Fetching data from URL: ", url);
        // const response = await axiosInstance.get(url);
        var response = await userServices.getUserById(userId);
        setUser(response.data);
        setError(null);
      } catch (error) {
        // console.error("Error fetching user details:", error);
        setError("Failed to fetch user data");
      }finally {
        setLoading(false);
      }
    }
    
    fetchData();
  }, [userId]);

  if (loading) {
    return <Typography>Loading...</Typography>;
  }

  if (error) {
    return (
      <Box>
        <Typography color="error">{error}</Typography>
        <Button
          variant="outlined"
          color="primary"
          onClick={() => window.location.reload()}
        >
          Retry
        </Button>
      </Box>
    );
  }

  return (
    <Box m={4} style={{ backgroundColor: colors.blueAccent[800], padding: '24px',  borderRadius: '12px' }}>
      <Typography variant="h3" gutterBottom style={{ fontWeight: "bold", marginBottom: '40px' }}>
        User Details
      </Typography>

      <Card elevation={4} style={{ backgroundColor: colors.blueAccent[700], marginBottom: '24px' }}>
        <CardContent>
          <Typography variant="h4" gutterBottom style={{ fontWeight: "bold", marginBottom: '20px' }}>
            Personal Info
          </Typography>
          <Box display="flex" alignItems="center" marginBottom={2}>
            <Avatar
            //   alt={`${user?.firstname} ${user?.lastname}`}
              alt={`${user?.userName}`}
            //   src={BASE_WEB_URL + user?.profileImageUrl}
            src={
                user?.roles.includes('teacher')
                  ? `${BASE_WEB_URL}/assets/uploads/images/teacher-images/${user?.image}`
                  : `${BASE_WEB_URL}/assets/uploads/images/student-images/${user?.image}`
              }
            //   src={`${BASE_WEB_URL}/assets/uploads/images/teacher-images/${user?.image}` || `${BASE_WEB_URL}/assets/uploads/images/student-images/${user?.image}`}
              style={{ width: '90px', height: '90px', marginRight: '24px' }}
            />
           <Box>
      <InfoPair label="ID:" value={user?.id} />
      {/* <InfoPair label="Last Name:" value={user?.lastname} /> */}
      <InfoPair label="Username:" value={user?.userName} />
      <InfoPair label="Email:" value={user?.email} />
    </Box>
          </Box>
        </CardContent>
      </Card>

      <Card elevation={4} style={{ backgroundColor: colors.blueAccent[700] }}>
        <CardContent>
          <Typography variant="h4" gutterBottom style={{ fontWeight: "bold",  marginBottom: '20px'}}>
            Roles
          </Typography>
          <Table>
            <TableHead>
            </TableHead>
            <TableBody>
  {(user?.roles || []).map((role, index) => (
    <TableRow key={index}>
      <TableCell>
        <Chip 
          key={role || index} 
          label={role} 
          variant="outlined"
          style={{ border: '1px solid', fontSize: "1rem" }}
        />
      </TableCell>
    </TableRow>
  ))}
</TableBody>
          </Table>
        </CardContent>
      </Card>
      {/* <Button
        variant="contained"
        style={{
          backgroundColor: colors.grey[700],
          fontSize: "0.8rem",
          fontWeight: "bold",
          marginTop: '20px',
          width: "100%"
        }}
        onClick={() => navigate(-1)} // navigating back to the previous page
      >
        Back to list
      </Button> */}
      <Button
          variant="outlined"
          color="secondary"
          sx={{
            marginLeft: "10px",
            marginTop: "20px",
            color: "#2196f3",
            border: "1px solid #2196f3",
            "&:hover": {
              color: "#3d5afe",
              backgroundColor: "transparent",
              border: "1px solid #2196f3",
            },
          }}
          onClick={() => {
            // Navigate to an edit page or open a modal/dialog for editing
            navigate(findPathByLabel(adminRoutes, "Users"));
          }}
        >
          Back to List
        </Button>
    </Box>
  );
}
