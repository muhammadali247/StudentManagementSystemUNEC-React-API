import styles from "./StudentsList.module.scss";
import { Box, Typography, useTheme } from "@mui/material";
import { DataGrid, GridToolbar } from "@mui/x-data-grid";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import Header from "../../../components/Admin/Header/Header";
import { useState, useEffect } from "react";
import axiosInstance from "../../../utils/axiosInstance";
import React from "react";
import { useNavigate } from "react-router-dom";
import adminRoutes from "../../../routing/adminRoutes";
import { findPathByLabel } from "../../../utils/routingUtils";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import ViewIcon from "@mui/icons-material/Visibility";
import Button from "@mui/material/Button";
import { darken } from "@mui/system";
import Tooltip from "../../../components/Global/UI/Tooltip";
// import {
//   EVENTS_GETALL_URL,
//   EVENTS_DELETE_URL,
//   BASE_WEB_URL,
// } from "@/constants/Urls";
import DeleteModal from "../../../components/Admin/Modals/DeleteModal";
import { useSnackbar } from "notistack";
import moment from "moment";
import LinearProgress from "@mui/material/LinearProgress";
import useService from "../../../hooks";

function UsersList() {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const navigate = useNavigate();
  const [deleteModalOpen, setDeleteModalOpen] = useState(false);
  //const [eventIdToDelete, setEventIdToDelete] = useState(null);
  const [studentIdToDelete, setStudentIdToDelete] = useState(null);
  const { enqueueSnackbar } = useSnackbar();
  const { userServices } = useService();
  const { studentServices } = useService();

  const handleAddStudent = () => {
    navigate(findPathByLabel(adminRoutes, "StudentsCreate"));
  };

  const viewAction = (id) => {
    console.log("View action triggered for ID:", id);
    navigate(findPathByLabel(adminRoutes, "StudentsDetails", { studentId: id }));
  };

  const editAction = (id) => {
    console.log("Edit action triggered for ID:", id);
    navigate(findPathByLabel(adminRoutes, "StudentsUpdate", { studentId: id }));
  };

  const deleteAction = (id) => {
    setStudentIdToDelete(id);
    setDeleteModalOpen(true);
  };

  const handleCloseDeleteModal = () => {
    setDeleteModalOpen(false);
    setStudentIdToDelete(null);
  };

  const handleDeleteEvent = async () => {
    if (!studentIdToDelete) return; // Ensuring there's an event ID

    try {
    //   var response = await userServices.deleteUser(userIdToDelete);
       var response = await studentServices.deleteStudent(studentIdToDelete);

       console.log(response);

       setData((prevData) =>
        prevData.filter((student) => student.id !== studentIdToDelete)
       );

       // Close the delete modal
       handleCloseDeleteModal();

       // Show a success notification to the user
       enqueueSnackbar("Student deleted successfully!", {
         variant: "success",
         autoHideDuration: 2000,
       });
    } catch (error) {
        console.error("Error deleting the student: ", error);

        // Check if the error has a response from the server
        if (error.response) {
          if (error.response.status === 500) {
            // Internal server error: Navigate to the error500 page
            navigate("/error500");
          } else if (error.response.data && error.response.data.errors) {
            // Other server-side validation errors: Show them in Snackbars
            const errorsFromServer = error.response.data.errors;
            Object.values(errorsFromServer).forEach((errorArray) => {
              errorArray.forEach((singleError) => {
                enqueueSnackbar(singleError, {
                  variant: "error",
                  autoHideDuration: 2000,
                });
              });
            });
          }
        } else {
          // Other axios errors (e.g., network errors)
          enqueueSnackbar("Failed to delete the student. Please try again.", {
            variant: "error",
            autoHideDuration: 2000,
          });
        }
    }
  };

  const columns = [
    { field: "id", headerName: "ID", width: 90 },
    { field: "name", headerName: "Name", flex: 0.12 },
    { field: "username", headerName: "Username", flex: 0.12 },
    { field: "surname", headerName: "Surname", flex: 0.12 },
    { field: "middleName", headerName: "Middlename", flex: 0.12 },
    { field: "country", headerName: "Country", flex: 0.125 },
    { field: "gender", headerName: "Gender", flex: 0.09 },
    // { field: "groupNames", headerName: "Study Groups", flex: 0.15 },
    { field: "educationStatus", headerName: "Education status", flex: 0.15 },
    { field: "mainGroupName", headerName: "Main Group", flex: 0.11 },
    {
        field: "groupNames",
        headerName: "Group Names",
        flex: 0.14,
        // renderCell: (params) => {
        //   // Convert the list of projects (which should be an array) to a sorted string
        //   const sortedProjects = [...params.value].sort(); // Sorting the array
        //   return sortedProjects.join("; ");
        // },
        renderCell: (params) => {
          // Check if params.value is an array
          if (Array.isArray(params.value)) {
            // Convert the list of projects (which should be an array) to a sorted string
            const sortedProjects = [...params.value].sort(); // Sorting the array
            return sortedProjects.join("; ");
          } else {
            // Handle the case where params.value is not an array
            return "N/A";
          }
        },
      },
    {
      field: "actions",
      headerName: "Actions",
      headerAlign: "center",
      cellClassName: styles.TableDataCell,
      width: 120,
      renderCell: (params) => (
        <div>
          <Tooltip content="View Student">
            <IconButton
              onClick={() => viewAction(params.id)}
              color="secondary"
              aria-label="view"
            >
              <ViewIcon />
            </IconButton>
          </Tooltip>
          <Tooltip content="Edit Student">
            <IconButton
              onClick={() => editAction(params.id)}
              color="secondary"
              aria-label="edit"
            >
              <EditIcon />
            </IconButton>
          </Tooltip>
          <Tooltip content="Delete Student">
            <IconButton
              onClick={() => deleteAction(params.id)}
              color="secondary"
              aria-label="delete"
            >
              <DeleteIcon />
            </IconButton>
          </Tooltip>
        </div>
      ),
    },
  ];

  useEffect(() => {
    async function fetchData() {
      try {
        // const response = await userServices.getAllUsers();
        const response = await studentServices.getAllStudents();
        setData(response.data);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching data: ", error);
        setLoading(false);
      }
    }

    fetchData();
  }, []);

  return (
    <Box m="20px">
      {/* HEADER */}
      <Box mb="5px">
        <Header title="Students" subtitle="The list of students!" />
        <Button
          variant="contained"
          sx={{
            width: "100%", // This makes the button span the full width
            backgroundColor: colors.blueAccent[500],
            "&:hover": {
              backgroundColor: darken(colors.blueAccent[600], 0.15),
            },
          }}
          onClick={handleAddStudent}
        >
          {" "}
          <Typography
            variant="h6"
            sx={{
              color:
                theme.palette.mode === "dark"
                  ? theme.palette.primary.contrastText
                  : theme.palette.secondary.contrastText,
              fontWeight: "bold",
            }}
          >
            Add New Student
          </Typography>
        </Button>
      </Box>
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
          rowHeight={70}
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
          rows={data}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: { pageSize: 10, page: 0 },
            },
          }}
          pageSizeOptions={[5, 10, 25, 100]}
        />
      </Box>
      <DeleteModal
        isOpen={deleteModalOpen}
        onClose={handleCloseDeleteModal}
        onDelete={handleDeleteEvent}
      />
    </Box>
  );
}

export default UsersList;