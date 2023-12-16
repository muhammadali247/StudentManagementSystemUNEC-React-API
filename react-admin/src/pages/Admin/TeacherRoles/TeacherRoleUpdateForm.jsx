import styles from "./TeacherRoleCreateForm.module.scss";
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
import { useSnackbar } from "notistack";
import useService from "../../../hooks";
import { TEACHER_ROLES } from "../../../constants/Enums";
import { jwtDecode } from "jwt-decode";

const initialValues = {
  name: "", // Teacher Name
};

function TeacherRoleUpdateForm() {
    const { enqueueSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const [serverErrors, setServerErrors] = useState({});
    const [existingData, setExistingData] = useState(null);
    const { teacherRoleServices } = useService();
    const { teacherRoleId } = useParams();
  
    const token = localStorage.getItem("accessToken");
  
  if (token) {
    // Use the token in your code
    console.log("Token:", token);
  } else {
    // Handle the case where the token is not found
    console.error("Token not found in localStorage");
  }

  // Fetch existing data when the component mounts
  useEffect(() => {
    async function fetchExistingData() {
      try {
        const response = await teacherRoleServices.getTeacherRoleById(teacherRoleId, token);
        setExistingData(response.data);
      } catch (error) {
        console.error("Error fetching existing data:", error);
      }
    }

    // fetchExistingData();
    if (token) {
      fetchExistingData();
      const decodedToken = jwtDecode(token);
  } else {
      console.error("Token not found in localStorage");
  }
  }, [teacherRoleId, token]);

  // Wait until the existing data is fetched
  if (!existingData) {
    return <div>Loading...</div>;
  }

  // Merge existing data with initial values
  const mergedValues = { ...initialValues, ...existingData };
  
    const handleFormSubmit = async (values, { resetForm }) => {
  
      // const formData = new FormData();
      // // Append all form fields to the FormData object
      // formData.append("name", values.name);

      const data = {
        name: values.name,
      };
  
      // for (const entry of formData.entries()) {
      //   console.log(entry);
      // }
  
        try {
          const response = await teacherRoleServices.updateTeacherRole(data, token);
          console.log("Success:", response.data);
          enqueueSnackbar("Teacher Role updated successfully!", {
            variant: "success",
            autoHideDuration: 5000,
          });
  
          // Reset the form after successful submission
          resetForm();
          
          navigate(findPathByLabel(adminRoutes, "TeacherRoles"));
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
              enqueueSnackbar("Error updating teacher role", { variant: "error" });
          }
      }
  
    };
  
      return (
      <Box m="20px">
        <Header title="Teacher roles" subtitle="Update Existing Record" />
        <Formik
          initialValues={mergedValues}
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
                      label="Teacher roles"
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
                      {TEACHER_ROLES.map((status) => (
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
  
  export default TeacherRoleUpdateForm;