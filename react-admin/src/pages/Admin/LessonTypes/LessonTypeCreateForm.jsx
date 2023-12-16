import styles from "./LessonTypeCreateForm.module.scss";
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
import { useSnackbar } from "notistack";
import useService from "../../../hooks";
import { LESSON_TYPES } from "../../../constants/Enums";

const initialValues = {
  name: "", // Teacher Name
};

function LessonTypeCreateForm() {
    const { enqueueSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const [serverErrors, setServerErrors] = useState({});
    const { teacherRoleServices } = useService();
    const { lessonTypeServices } = useService();
  
    const token = localStorage.getItem("accessToken");
  
  if (token) {
    // Use the token in your code
    console.log("Token:", token);
  } else {
    // Handle the case where the token is not found
    console.error("Token not found in localStorage");
  }
  
    const handleFormSubmit = async (values, { resetForm }) => {
      const data = {
        name: values.name,
      };
  
        try {
          const response = await lessonTypeServices.createLessonType(data, token);
          console.log("Success:", response.data);
          enqueueSnackbar("Lesson Type added successfully!", {
            variant: "success",
            autoHideDuration: 5000,
          });
  
          // Reset the form after successful submission
          resetForm();
          
          navigate(findPathByLabel(adminRoutes, "LessonTypes"));
        } catch (error) {
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
              enqueueSnackbar("Error adding lesson type", { variant: "error" });
          }
      }
  
    };
  
      return (
      <Box m="20px">
        <Header title="Lesson types" subtitle="Create New Record" />
        <Formik
          initialValues={initialValues}
          // validationSchema={EventCreateSchema}
  
          // onSubmit={handleFormSubmit}
          onSubmit={(values, { resetForm }) => handleFormSubmit(values, { resetForm })}
        >
          {({
            values,
            errors,
            touched,
            handleBlur,
            handleChange,
            handleSubmit,
            setFieldValue,
            setFieldTouched,
          }) => (
            <form onSubmit={handleSubmit}>
              <Box
                display="flex"
                flexDirection="column"
                gap="1rem"
                width="90%"
                px="2rem"
                py="1.5rem"
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
  
                <Box className={styles.InputRow}>
                  <TextField
                    fullWidth
                    variant="filled"
                    select
                    label="Lesson types"
                    onBlur={handleBlur}
                    onChange={handleChange}
                    value={values.name}
                    name="name"
                    error={!!touched.name && !!errors.name}
                    helperText={
                      touched.name && errors.name
                        ? errors.name
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
                    {LESSON_TYPES.map((status) => (
                      <MenuItem key={status} value={status}>
                        {status}
                      </MenuItem>
                    ))}
                </TextField>
                </Box>
  
                <Box className={styles.InputRow}>
                  {/* Submit Button */}
                  <Button
                    fullWidth
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
                    fullWidth
                    variant="contained"
                    style={{
                      backgroundColor: colors.grey[500],
                      fontSize: "0.8rem",
                      fontWeight: "bold",
                    }}
                  //   onClick={() => navigate(-1)} // navigating back to the previous page
                  onClick={() => navigate(findPathByLabel(adminRoutes, "TeacherRoles"))} // navigating back to the previous page
                  >
                    Cancel
                  </Button>
                </Box>
              </Box>
            </form>
          )}
        </Formik>
      </Box>
    );
  }
  
  export default LessonTypeCreateForm;