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
  
  function SubjectCreateForm() {
    const { enqueueSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const [serverErrors, setServerErrors] = useState({});
    const { subjectServices } = useService();

    const handleFormSubmit = async (values) => {
      const requestData = {
        name: values.name,
        subjectCode: values.subjectCode,
        semester: values.semester,
      };

      try {
        // Make your API request using formData
        // const response = await facultyServices.createFaculty( formData );
        const response = await subjectServices.createSubject( requestData );
    
        console.log("Success:", response.data);
        enqueueSnackbar("Subject added successfully!", {
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
          enqueueSnackbar("Error adding subject", { variant: "error" });
        }
      }
    };
    
  
    return (
      <Box m="20px">
      <Header title="Subjects" subtitle="Create New Record" />
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

              {/* <TextField
                fullWidth
                variant="filled"
                type="text"
                label="Semester"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.semester || ''}
                name="semester"
                error={!!touched.semester && !!errors.semester}
                helperText={touched.semester && errors.semester}
                // Changes for increasing font size of label and helperText
                InputLabelProps={{
                  style: { fontSize: "1rem" },
                  focused: false,
                }} // Increase font size of label
                FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
              /> */}
              
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

  export default SubjectCreateForm;