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
import { jwtDecode } from "jwt-decode";

const InfoPair = ({ label, value }) => {
  return (
    <Box display="flex" alignItems="center" marginBottom={1}>
      <Typography variant="h5" style={{ minWidth: '120px', fontWeight: 'bold' }}>{label}</Typography>
      <Typography variant="h5" style={{ fontWeight: 'normal' }}>{value}</Typography>
    </Box>
  );
};

export default function UsersDetailsForm() {
  const [student, setStudent] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const { studentId } = useParams();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const navigate = useNavigate();
  const { userServices } = useService();

  const { studentServices } = useService();

  const token = localStorage.getItem("accessToken");

  if (token) {
      const decodedToken = jwtDecode(token);
  }

  useEffect(() => {
    async function fetchData() {
      try {
        setLoading(true);
        // var response = await studentServices.getStudentById(studentId, token);
        var response = await studentServices.getStudentById(studentId);
        setStudent(response.data);
        setError(null);
      } catch (error) {
        // console.error("Error fetching user details:", error);
        setError("Failed to fetch student data");
      }finally {
        setLoading(false);
      }
    }
    
    fetchData();
  }, [studentId]);

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
        Student Details
      </Typography>

      <Card elevation={4} style={{ backgroundColor: colors.blueAccent[700], marginBottom: '24px' }}>
        <CardContent>
          <Typography variant="h4" gutterBottom style={{ fontWeight: "bold", marginBottom: '20px' }}>
            Personal Info:
          </Typography>
          <Box display="flex" alignItems="center" marginBottom={2}>
            <Avatar
            //   alt={`${user?.firstname} ${user?.lastname}`}
              alt={`${student?.username}`}
            //   src={BASE_WEB_URL + user?.profileImageUrl}
            src={
                // student?.roles.includes('teacher')
                //   ? `${BASE_WEB_URL}/assets/uploads/images/teacher-images/${student?.image}`
                //   : `${BASE_WEB_URL}/assets/uploads/images/student-images/${student?.image}`
                `${BASE_WEB_URL}/assets/uploads/images/student-images/${student?.image}`
              }
            //   src={`${BASE_WEB_URL}/assets/uploads/images/teacher-images/${user?.image}` || `${BASE_WEB_URL}/assets/uploads/images/student-images/${user?.image}`}
              style={{ width: '100px', height: '100px', marginRight: '24px' }}
            />
           <Box>
                {/* <InfoPair label="ID:" value={student?.id} /> */}
                
                {/* <InfoPair label="Last Name:" value={user?.lastname} /> */}
                <InfoPair label="Username:" value={student?.username} />
                <InfoPair label="Admission Year:" value={student?.admissionYear} />

                {/* <InfoPair label="Email:" value={user?.email} /> */}
                <InfoPair label="Name:" value={student?.name} />
                <InfoPair label="Surname:" value={student?.surname} />
                <InfoPair label="Middlename:" value={student?.middleName} />
                <InfoPair label="Gender:" value={student?.gender} />
                {/* <InfoPair label="Birth Date:" value={student?.birthDate} />
                <InfoPair label="Origin Country:" value={student?.country} />
                <InfoPair label="Corporative Email:" value={student?.corporativeEmail} />
                <InfoPair label="Education status:" value={student?.educationStatus} />
                <InfoPair label="Corporative Email:" value={student?.corporativeEmail} />
                <InfoPair label="Admission Year:" value={student?.admissionYear} /> */}
                </Box>
                <Box style={{marginLeft: "30px"}}>
                <InfoPair label="Birth Date:" value={student?.birthDate} />
                <InfoPair label="Origin Country:" value={student?.country} />
                <InfoPair label="Corporative Email:" value={student?.corporativeEmail} />
                <InfoPair label="Education status:" value={student?.educationStatus} />
                <InfoPair label="Corporative Email:" value={student?.corporativeEmail} />
                {/* <InfoPair label="Admission Year:" value={student?.admissionYear} /> */}
                </Box>
          </Box>
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
