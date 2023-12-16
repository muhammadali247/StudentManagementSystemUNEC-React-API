import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import adminRoutes from "../../../routing/adminRoutes";
import { findPathByLabel } from "../../../utils/routingUtils";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import {
  useTheme,
  Box,
  Button,
  TextField,
  Paper,
  Typography,
} from "@mui/material";
import { Formik } from "formik";
import * as yup from "yup";
import useMediaQuery from "@mui/material/useMediaQuery";
import Header from "../../../components/Admin/Header/Header";
import axiosInstance from "../../../utils/axiosInstance";
import styles from "./FacultyCreateForm.module.scss";
import { useSnackbar } from "notistack";
import {
  ARTIST_CREATE_URL,
  MUSICAL_PROJECTS_GETALL_URL,
} from "../../../constants/URLs";
import Autocomplete from "@mui/material/Autocomplete";
import Chip from "@mui/material/Chip";
import { ArtistCreateSchema, FacultyCreateSchema } from "../../../validations/FormSchemas";
import useService from "../../../hooks";
import axios from "axios";


const initialValues = {
    facultyName: "",
    facultyCode: "",
    studySectorName: "",
    studySectorCode: "",
  };
  
  function FacultyCreateForm() {
    const { enqueueSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const [serverErrors, setServerErrors] = useState({});
    const { facultyServices } = useService();

    const handleFormSubmit = async (values) => {
      // const formData = new FormData();
      // formData.append("name", values.name); // Corrected field name
      // formData.append("facultyCode", values.facultyCode); // Corrected field name
      // formData.append("studySectorName", values.studySectorName); // Corrected field name
      // formData.append("studySectorCode", values.studySectorCode); // Add this line if needed
    
      // console.log(formData);
      const requestData = {
        name: values.name,
        facultyCode: values.facultyCode,
        studySectorName: values.studySectorName,
        studySectorCode: values.studySectorCode,
      };
      try {
        // Make your API request using formData
        // const response = await facultyServices.createFaculty( formData );
        const response = await facultyServices.createFaculty( requestData );
    
        console.log("Success:", response.data);
        enqueueSnackbar("Faculty added successfully!", {
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
          enqueueSnackbar("Error adding faculty", { variant: "error" });
        }
      }
    };
    
  
    return (
      <Box m="20px">
      <Header title="Faculties" subtitle="Create New Record" />
      <Formik
        initialValues={initialValues}
        onSubmit={handleFormSubmit}
        validator={() => ({})}
        // validationSchema={FacultyCreateSchema}
      >
        {({
          values,
          errors,
          touched,
          handleBlur,
          handleChange,
          handleSubmit,
          // setFieldValue,
          // setFieldTouched,
        }) => (
          <form onSubmit={(e) => handleSubmit(e)}>
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
                  <Box
                    bgcolor="error.main"
                    color="white"
                    p={2}
                    borderRadius={2}
                  >
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

              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Faculty name"
                onBlur={handleBlur}
                onChange={handleChange}
                // value={values.name}
                value={values.name || ''}
                name="name"
                error={!!touched.name && !!errors.name}
                helperText={touched.name && errors.name}
                // Changes for increasing font size of label and helperText
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }} // Increase font size of label
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
              />

              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Faculty Code"
                onBlur={handleBlur}
                onChange={handleChange}
                // value={values.facultyCode}
                value={values.facultyCode || ''}
                name="facultyCode"
                error={!!touched.facultyCode && !!errors.facultyCode}
                helperText={touched.facultyCode && errors.facultyCode}
                // Changes for increasing font size of label and helperText
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }} // Increase font size of label
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
              />

              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Study Sector Name"
                onBlur={handleBlur}
                onChange={handleChange}
                // value={values.studySectorName}
                value={values.studySectorName || ''}
                name="studySectorName"
                error={!!touched.studySectorName && !!errors.studySectorName}
                helperText={touched.studySectorName && errors.studySectorName}
                // Changes for increasing font size of label and helperText
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }} // Increase font size of label
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
              />

              <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Study Sector Code"
                onBlur={handleBlur}
                onChange={handleChange}
                // value={values.studySectorCode}
                value={values.studySectorCode || ''}
                name="studySectorCode"
                error={!!touched.studySectorCode && !!errors.studySectorCode}
                helperText={touched.studySectorCode && errors.studySectorCode}
                // Changes for increasing font size of label and helperText
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }} // Increase font size of label
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
              />
              
              {/* Submit Button */}
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
              {/* Cancel Button */}
              <Button
                variant="contained"
                style={{
                  backgroundColor: colors.grey[500],
                  fontSize: "0.8rem",
                  fontWeight: "bold",
                }}
                onClick={() => navigate(-1)} // navigating back to the previous page
              >
                Cancel
              </Button>
            </Box>
          </form>
        )}
      </Formik>
    </Box>
    );
  }

  export default FacultyCreateForm;