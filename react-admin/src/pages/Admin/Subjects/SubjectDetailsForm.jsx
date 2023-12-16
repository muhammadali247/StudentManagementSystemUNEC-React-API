import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Box, Button, Typography, Paper, List, ListItem, useTheme } from "@mui/material";
import axiosInstance from "../../../utils/axiosInstance";
import useService from "../../../hooks";
import adminRoutes from "../../../routing/adminRoutes";
import { findPathByLabel } from "../../../utils/routingUtils";
import Header from "../../../components/Admin/Header/Header";

import { DataGrid, GridToolbar } from "@mui/x-data-grid";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import LinearProgress from "@mui/material/LinearProgress";

function SubjectDetailsForm() {
  const { subjectId } = useParams();
  const navigate = useNavigate();
  const { subjectServices } = useService();

  const [subjectData, setSubjectData] = useState(null);

  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [loading, setLoading] = useState(true);
  
  const token = localStorage.getItem("accessToken");

    if (token) {
      // Use the token in your code
      console.log("Token:", token);
    } else {
      // Handle the case where the token is not found
      console.error("Token not found in localStorage");
    }

  useEffect(() => {
    async function fetchData() {
      try {
        var response = await subjectServices.getSubjectById(subjectId, token);
        setSubjectData(response.data);
      } catch (error) {
        console.error("Error fetching subject details:", error);
      }
    }

    fetchData();
  }, [subjectId]);

  if (!subjectData) {
    return <div>Loading...</div>;
  }

  const columns = [
    { field: "id", headerName: "ID", flex: 0.3 },
    { field: "name", headerName: "Subject Name", flex: 0.25 },
    { field: "subjectCode", headerName: "Subject Code", flex: 0.25 },
    { field: "semester", headerName: "Semester", flex: 0.25 },
  ];

  return (
    <Box m="20px">
      <Paper elevation={3} sx={{ padding: "20px", backgroundColor: "#1f2a40" }}>
        <Header title={`${subjectData.name}`} subtitle="The list of subjects!"/>
        <Box
        m="0 0 0 0"
        height="520px"
        sx={{
          "& .MuiDataGrid-root": {},
          "& .MuiDataGrid-cell": {
            fontSize: "0.9rem",
            borderRight: "2px solid rgba(0, 0, 0, 0.1)",
          },
          "& .MuiDataGrid-columnHeaders": {
            backgroundColor: colors.blueAccent[700],
            fontSize: "1rem",
          },
          "& .MuiDataGrid-columnHeader": {
            borderRight: "2px solid #003366",
          },
          "& .MuiDataGrid-columnHeaderTitle": {
            fontWeight: "bold",
          },
          "& .MuiDataGrid-virtualScroller": {
            backgroundColor: colors.primary[400],
          },
          "& .MuiDataGrid-footerContainer": {
            backgroundColor: colors.blueAccent[700],
            // display: "none"
          },
          "& .MuiDataGrid-toolbarContainer": {
            "& .MuiButton-root, & .MuiIconButton-root, & .MuiTypography-root": {
              color: colors.grey[100],
            },
          },
        }}
      >
        <DataGrid
          loading={loading}
          slots={{
            toolbar: GridToolbar,
            loadingOverlay: LinearProgress,
          }}
          slotProps={{
            toolbar: {
              showQuickFilter: true,
              quickFilterProps: { debounceMs: 500 },
            },
          }}
          rows={subjectData}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: { pageSize: 10, page: 0 },
            },
          }}
          pageSizeOptions={[5, 10, 25, 100]}
        />
      </Box>

        <Button
          variant="outlined"
          color="secondary"
          sx={{
            marginLeft: "10px",
            marginTop: "10px",
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
            navigate(findPathByLabel(adminRoutes, "Subjects"));
          }}
        >
          Back to List
        </Button>
      </Paper>
    </Box>
  );
}

export default SubjectDetailsForm;