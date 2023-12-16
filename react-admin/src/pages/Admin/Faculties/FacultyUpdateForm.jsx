import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useSnackbar } from "notistack"; // Import the useSnackbar hook.
import adminRoutes from "../../../routing/adminRoutes";
import { findPathByLabel } from "../../../utils/routingUtils";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import {
  useTheme,
  Box,
  Button,
  TextField,
  Typography,
} from "@mui/material";
import { Formik } from "formik";
import axiosInstance from "../../../utils/axiosInstance";
import LoadingIndicator from "../../../components/Global/UI/LoadingIndicator";
import Header from "../../../components/Admin/Header/Header";
import styles from "./FacultyUpdateForm.module.scss";
import useService from "../../../hooks";

const initialValues = {
  name: "",
  facultyCode: "",
  studySectorName: "",
  studySectorCode: "",
};

function FacultyUpdateForm() {
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const { enqueueSnackbar } = useSnackbar(); // Initialize the useSnackbar hook.
  const { facultyId } = useParams();
  const [facultyInitialValues, setFacultyInitialValues] = useState(initialValues);
  const [serverErrors, setServerErrors] = useState({});
  const { facultyServices } = useService();
  const [isDataLoaded, setIsDataLoaded] = useState(false);

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await facultyServices.getFacultyById(facultyId);
        setFacultyInitialValues({
          name: response.data.name,
          facultyCode: response.data.facultyCode,
          studySectorName: response.data.studySectorName,
          studySectorCode: response.data.studySectorCode,
        });
        setIsDataLoaded(true);
      } catch (error) {
        enqueueSnackbar("Error fetching faculty details", { variant: "error" });
      }
    }

    fetchData();
  }, [facultyId, enqueueSnackbar]);

  const handleFormSubmit = async (values) => {
    setServerErrors({});

    const formData = new FormData();
    formData.append("name", values.name);
    formData.append("facultyCode", values.facultyCode);
    formData.append("studySectorName", values.studySectorName);
    formData.append("studySectorCode", values.studySectorCode);

    try {
      const response = await facultyServices.updateFaculty(facultyId, formData);
      console.log("Success:", response.data);
      enqueueSnackbar("Faculty updated successfully!", {
        variant: "success",
        autoHideDuration: 5000,
      });
      navigate(findPathByLabel(adminRoutes, "Faculties"));
    } catch (error) {
      console.log(error);
      if (error.response) {
        if (error.response.status === 500) {
          navigate("/error500");
        } else if (error.response.data && error.response.data.errors) {
          const errorsFromServer = error.response.data.errors;
          setServerErrors(errorsFromServer);
          enqueueSnackbar("Please correct the highlighted errors.", {
            variant: "error",
          });
        }
      } else {
        enqueueSnackbar("Error updating faculty", { variant: "error" });
      }
    };
  }

  return (
    <Box m="20px">
      <Header title="Faculties" subtitle="Update Record" />

      <Formik
        key={facultyId}
        initialValues={facultyInitialValues}
        // Add validationSchema if needed
        onSubmit={handleFormSubmit}
      >
        {({
          values,
          errors,
          touched,
          handleBlur,
          handleChange,
          handleSubmit,
        }) => (
          <Box
            display="flex"
            flexDirection="column"
            gap="1rem"
            width="100%"
            p="2rem"
            borderRadius="12px"
            border={`1px solid ${theme.palette.divider}`}
            bgcolor={colors.primary[400]}
            boxShadow={3}
          >
            <Box>
              {Object.keys(serverErrors).length > 0 && (
                <Box bgcolor="error.main" color="white" p={2} borderRadius={2}>
                  <Typography variant="h5">Errors:</Typography>
                  <ul>
                    {Object.entries(serverErrors).map(([field, errors]) => (
                      <li key={field}>
                        {errors.map((error, index) => (
                          <div key={index} className={styles.serverError}>
                            {error}
                          </div>
                        ))}
                      </li>
                    ))}
                  </ul>
                </Box>
              )}
            </Box>
            <form onSubmit={handleSubmit}>
              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Faculty name"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.name}
                name="name"
                error={!!touched.name && !!errors.name}
                helperText={touched.name && errors.name}
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }}
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
              />

              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Faculty Code"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.facultyCode}
                name="facultyCode"
                error={!!touched.facultyCode && !!errors.facultyCode}
                helperText={touched.facultyCode && errors.facultyCode}
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }}
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
              />

              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Study Sector Name"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.studySectorName}
                name="studySectorName"
                error={!!touched.studySectorName && !!errors.studySectorName}
                helperText={touched.studySectorName && errors.studySectorName}
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }}
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
              />

              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Study Sector Code"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.studySectorCode}
                name="studySectorCode"
                error={!!touched.studySectorCode && !!errors.studySectorCode}
                helperText={touched.studySectorCode && errors.studySectorCode}
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }}
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
              />

              <Box mt={2} display="flex" flexDirection="column" gap="1rem">
                <Button
                  type="submit"
                  variant="contained"
                  style={{
                    backgroundColor: colors.blueAccent[400],
                    fontSize: "0.8rem",
                    fontWeight: "bold",
                  }}
                >
                  Submit
                </Button>
                <Button
                  variant="contained"
                  style={{
                    backgroundColor: colors.grey[500],
                    fontSize: "0.8rem",
                    fontWeight: "bold",
                  }}
                  onClick={() => navigate(-1)}
                >
                  Cancel
                </Button>
              </Box>
            </form>
          </Box>
        )}
      </Formik>
    </Box>
  );
}

export default FacultyUpdateForm;
