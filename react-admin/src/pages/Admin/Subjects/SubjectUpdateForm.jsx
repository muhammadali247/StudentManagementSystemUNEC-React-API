import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import adminRoutes from "../../../routing/adminRoutes";
import { findPathByLabel } from "../../../utils/routingUtils";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import {
  useTheme,
  Box,
  Button,
  TextField,
  Typography,
  MenuItem,
} from "@mui/material";
import { Formik } from "formik";
import Header from "../../../components/Admin/Header/Header";
import styles from "./SubjectCreateForm.module.scss";
import { useSnackbar } from "notistack";
import useService from "../../../hooks";
import { SEMESTERS } from "../../../constants/Enums";

const initialValues = {
    name: "",
    subjectCode: "",
    semester: "",
  };
  
  function SubjectUpdateForm() {
    const { enqueueSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const [serverErrors, setServerErrors] = useState({});
    const [existingData, setExistingData] = useState(null);
    const { subjectServices } = useService();
    const { subjectId } = useParams();

    const token = localStorage.getItem("accessToken");

    if (token) {
      // Use the token in your code
      console.log("Token:", token);
    } else {
      // Handle the case where the token is not found
      console.error("Token not found in localStorage");
    }
      useEffect(() => {
        async function fetchExistingData() {
          try {
            const response = await subjectServices.getSubjectById(subjectId, token);
            setExistingData(response.data);
          } catch (error) {
            console.error("Error fetching existing data:", error);
          }
        }
  
        // fetchExistingData();
        if (token) {
          fetchExistingData();
      } else {
          console.error("Token not found in localStorage");
      }
      }, [subjectId, token]);

      // Wait until the existing data is fetched
    if (!existingData) {
        return <div>Loading...</div>;
      }
  
      // Merge existing data with initial values
      const mergedValues = { ...initialValues, ...existingData };

    const handleFormSubmit = async (values) => {
      const requestData = {
        name: values.name,
        subjectCode: values.subjectCode,
        semester: values.semester,
      };

      try {
        // Make your API request using formData
        // const response = await facultyServices.createFaculty( formData );
        const response = await subjectServices.updateSubject( subjectId, requestData, token );
    
        console.log("Success:", response.data);
        enqueueSnackbar("Subject updated successfully!", {
          variant: "success",
          autoHideDuration: 5000,
        });
        navigate(findPathByLabel(adminRoutes, "Subjects"));
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
          enqueueSnackbar("Error updating subject", { variant: "error" });
        }
      }
    };
    
  
    return (
      <Box m="20px">
      <Header title="Subjects" subtitle="Update Existing Record" />
      <Formik
        initialValues={mergedValues}
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
                label="Subject name"
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
                label="Subject Code"
                onBlur={handleBlur}
                onChange={handleChange}
                // value={values.facultyCode}
                value={values.subjectCode || ''}
                name="subjectCode"
                error={!!touched.subjectCode && !!errors.subjectCode}
                helperText={touched.subjectCode && errors.subjectCode}
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
                    select
                    label="Semester"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.semester}
                    name="semester"
                    error={!!touched.semester && !!errors.semester}
                    helperText={
                      touched.semester && errors.semester
                        ? errors.semester
                        : "\u00a0"
                    }
                    InputLabelProps={{
                      style: { fontSize: "1rem" },
                      focused: false,
                    }}
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
                    SelectProps={{
                      MenuProps: {
                        PaperProps: {
                          style: {
                            backgroundColor: colors.blueAccent[700],
                          },
                        },
                      },
                    }}
                >
                    {SEMESTERS.map((status) => (
                      <MenuItem key={status} value={status}>
                        {status}
                      </MenuItem>
                    ))}
                </TextField>
              
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
  export default SubjectUpdateForm;