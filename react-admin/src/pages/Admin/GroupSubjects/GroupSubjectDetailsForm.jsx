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

function GroupSubjectsDetails() {
  const { groupSubjectId } = useParams();
  const navigate = useNavigate();
  const { facultyServices } = useService();
  const { groupSubjectServices } = useService();

  const [groupSubjectData, setGroupSubjectData] = useState(null);
  const [facGroupData, setFacGroupData] = useState(null);

  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [loading, setLoading] = useState(true);
  

  useEffect(() => {
    async function fetchData() {
      try {
        var response = await groupSubjectServices.getGroupSubjectById(groupSubjectId);
        setGroupSubjectData(response.data);
        // setFacGroupData(response.data.groups);
      } catch (error) {
        console.error("Error fetching group subject details:", error);
      }
    }

    fetchData();
  }, [groupSubjectId]);

  if (!groupSubjectData) {
    return <div>Loading...</div>;
  }

  const columns = [
    { field: "id", headerName: "ID", flex: 0.3 },
    { field: "subjectName", headerName: "Subject Name", flex: 0.25 },
    { field: "subjectCode", headerName: "Subject Code", flex: 0.25 },
    { field: "semester", headerName: "Semester", flex: 0.25 },
    { field: "groupName", headerName: "Group Name", flex: 0.25 },
    { field: "groupFaculty", headerName: "Group Faculty", flex: 0.25 },
    { field: "studentCount", headerName: "Student Count", flex: 0.25 },
    { field: "credits", headerName: "Credits(subject)", flex: 0.25 },
    { field: "totalHours", headerName: "Hours(total)", flex: 0.25 },
  ];

  return (
    <Box m="20px">
      <Paper elevation={3} sx={{ padding: "20px", backgroundColor: "#1f2a40" }}>
        <Header title={`Group-subject`} subtitle="The list of group subjects!"/>
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
          rows={groupSubjectData}
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
            navigate(findPathByLabel(adminRoutes, "GroupSubjects"));
          }}
        >
          Back to List
        </Button>
      </Paper>
    </Box>
  );
}

export default GroupSubjectsDetails;