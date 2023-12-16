import React from "react";
import Card from "@mui/material/Card";
import Button from "@mui/material/Button";
import { Checkbox, TextField, useTheme, Typography, Paper, } from "@mui/material";
import { Form, Formik, useField } from "formik";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import { ReactComponent as Logo } from "../../../assets/icons/signup.svg";
import { SignUpSchema } from "../../../validations/FormSchemas";
import axios from "axios";
import { ACCOUNT_REGISTER_URL } from "../../../constants/URLs";
import useService from "../../../hooks";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "notistack";
import { findPathByLabel } from "../../../utils/routingUtils";
import authRoutes from "../../../routing/authRoutes";
import { useState,useEffect } from "react";
import { colors } from "@mui/material";
import Autocomplete from "@mui/material/Autocomplete";
import Chip from "@mui/material/Chip";
import { tokens } from "../../../layouts/Admin/AdminTheme";
import { useFormik } from 'formik';

const AppTextField = (props) => {
  const [field, meta, helpers] = useField(props);
  const errorText = meta.error && meta.touched ? meta.error : " ";
  return (
    <TextField
      {...props}
      {...field}
      helperText={errorText}
      error={meta.touched && !!meta.error}
      onBlur={helpers.blur}  // Pass the blur function from helpers
      sx={{
        width: "100%",
        mb: 1,
      }}
    />
  );
};


const initialValues = {
  userName: "",
  email: "",
  password: "",
  confirmPassword: "",
  // roleIds: [],
};

function SignUp() {
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [serverErrors, setServerErrors] = React.useState({});
  // const [roles, setRoles] = useState([]);
  const [userId, setUserId] = useState("");
  const { userServices } = useService();
  const { roleServices } = useService();

  // useEffect(() => {
  //   async function fetchData() {
  //     try {
  //       const response = await roleServices.getAllRoles();
  //       setRoles(response.data);
  //     } catch (error) {
  //       console.error("Error fetching roles:", error);
  //     }
  //   }

  //   fetchData();
  // }, []);


  const handleFormSubmit = async (
    values,
    { setSubmitting, setErrors, resetForm }
  ) => {
    setSubmitting(true);
    // Validate that the password and confirm password fields match
    if (values.password !== values.confirmPassword) {
      setErrors({
        confirmPassword: "Password Mismatch",
      });
      setSubmitting(false);
      return;
    }

    const data = {
      userName: values.userName,
      email: values.email,
      password: values.password,
      confirmPassword: values.confirmPassword,
      // roleIds: values.roleIds.map(role => role.id)
    };

    try {
      const response = await userServices.createStudentUser(data);
      console.log("Registration successful:", response.data);
      setUserId(response.data);
      enqueueSnackbar("Account created successfully!", {
        variant: "success",
        autoHideDuration: 5000,
      });
      enqueueSnackbar("The verification code has been sent to you email!", {
        variant: "info",
        autoHideDuration: 5000,
      });
      // navigate(findPathByLabel(authRoutes, "VerifyAccount", { userId: userId }));
      navigate(findPathByLabel(authRoutes, "VerifyAccount", { userId: response.data }));
      // navigate(`/auth/verifyAccount/${userId}`);
      // navigate(findPathByLabel(adminRoutes, "FacultiesDetails", { facultyId: id }));
    } catch (error) {
      let errorMessage = "An error occurred while registering.";
      if (error.response) {
        if (error.response.status === 500) {
          navigate("/error500");
          return;
        } else if (error.response.data && error.response.data.errors) {
          const errorsFromServer = error.response.data.errors;
          console.log('Server errors:', errorsFromServer);
          setErrors(errorsFromServer);
          setServerErrors(errorsFromServer); // Store the server errors
          errorMessage = "Please correct the highlighted errors.";
        }        
      }
      enqueueSnackbar(errorMessage, {
        variant: "error",
      });
    }finally {
      setSubmitting(false);
    }
  };

  // const formik = useFormik({
  //   initialValues: initialValues,
  //   // validationSchema: SignUpSchema, // You can add your validation schema here
  //   onSubmit: handleFormSubmit,
  // });

  // const {
  //   isSubmitting,
  //   values,
  //   errors,
  //   touched,
  //   handleBlur,
  //   handleChange,
  // } = formik;

  return (
    <Box
      sx={{
        padding: "1rem",
        display: "flex",
        minHeight: "100vh",
        flex: 1,
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <Card
        sx={{
          maxWidth: 1024,
          borderRadius: "2%",
          width: "100%",
          backgroundColor: "#1F2A40",
          paddingX: 8,
          paddingY: 3,
          paddingLeft: { xs: 8, md: 2 },
          overflow: "hidden",
          boxShadow:
            "0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)",
        }}
      >
        <Grid
          container
          spacing={5}
          sx={{
            alignItems: { md: "center" },
          }}
        >
          <Grid
            item
            xs={12}
            md={6}
            sx={{
              textAlign: "center",
            }}
          >
            <Logo fill={theme.palette.primary.main} />
          </Grid>
          <Grid
            item
            xs={12}
            md={6}
            sx={{
              textAlign: "center",
            }}
          >
            <Box
              sx={{
                mb: { xs: 6, xl: 3 },
                fontWeight: 700,
                fontSize: 30,
              }}
            >
              Sign Up
            </Box>
            {Object.keys(serverErrors).length > 0 && (
              <Box bgcolor="error.main" color="white" p={2} borderRadius={2}  sx={{
                mb: { xs: 6, xl: 3 }}}>
                <Typography variant="h5">Errors:</Typography>
                <ul>
                  {Object.entries(serverErrors).map(([field, errors]) => (
                    <li key={field}>
                      {errors.map((error, index) => (
                        <div key={index}>{error}</div>
                      ))}
                    </li>
                  ))}
                </ul>
              </Box>
            )}
            
            <Formik
              initialValues={initialValues}
              // Add validationSchema if needed
              validationSchema= {SignUpSchema}
              // validator={() => ({})}
              onSubmit={handleFormSubmit}
            >
            {({
              isSubmitting,
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
                <TextField
                  fullWidth
                  variant="filled"
                  type="text"
                  label="User Name"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.userName}
                  name="userName"
                  error={!!touched.userName && !!errors.userName}
                  helperText={touched.userName && errors.userName}
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
                  label="Email"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.email}
                  name="email"
                  error={!!touched.email && !!errors.email}
                  helperText={touched.email && errors.email}
                  InputLabelProps={{
                    style: { fontSize: "1rem" },
                    focused: false,
                  }}
                  FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
                />

                <TextField
                  fullWidth
                  variant="filled"
                  type="password"
                  label="Password"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.password}
                  name="password"
                  error={!!touched.password && !!errors.password}
                  helperText={touched.password && errors.password}
                  InputLabelProps={{
                    style: { fontSize: "1rem" },
                    focused: false,
                  }}
                  FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
                />

                <TextField
                  fullWidth
                  variant="filled"
                  type="password"
                  label="Confirm Password"
                  onBlur={handleBlur}
                  onChange={handleChange}
                  value={values.confirmPassword}
                  name="confirmPassword"
                  error={!!touched.confirmPassword && !!errors.confirmPassword}
                  helperText={touched.confirmPassword && errors.confirmPassword}
                  InputLabelProps={{
                    style: { fontSize: "1rem" },
                    focused: false,
                  }}
                  FormHelperTextProps={{ style: { fontSize: "0.9rem" } }}
                />

              {/* <Autocomplete
                PaperComponent={({ children, ...other }) => (
                  <Paper
                    {...other}
                    sx={{ backgroundColor: colors.blueAccent[700] }}
                  >
                    {children}
                  </Paper>
                )}
                multiple
                id="roleIds"
                options={roles}
                getOptionLabel={(option) => option.name}
                value={values.roleIds}
                onChange={(_, newValue) => {
                  setFieldValue("roleIds", newValue);
                  setFieldTouched("roleIds", true); // Set the touch status
                }}
                onBlur={() => setFieldTouched("roleIds", true)} // Ensure that field is marked touched on blur.
                renderInput={(params) => (
                  <TextField
                    {...params}
                    variant="filled"
                    label="Select Roles for AppUser"
                    error={!!touched.roleIds && !!errors.roleIds}
                    helperText={touched.roleIds && errors.roleIds}
                    InputLabelProps={{
                      style: { fontSize: "1rem" },
                      focused: false,
                    }}
                    FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                  />
                )}
                renderTags={(selectedRoles, getTagProps) =>
                  selectedRoles.map((specRole, index) => (
                    <Chip
                      key={specRole.id} // using the unique project ID as the key
                      variant="outlined"
                      label={specRole.name} // displaying the project name on the chip
                      {...getTagProps({ index })}
                    />
                  ))
                }
              /> */}
              <Button
                    variant="contained"
                    color="primary"
                    disabled={isSubmitting}
                    sx={{
                      width: "100%",
                      height: 44,
                    }}
                    type="submit"
                  >
                    Sign Up
                  </Button>
              </form>
            )}
            </Formik>


            <Box
              sx={{
                textAlign: "center",
                color: "grey.500",
                fontSize: 14,
                fontWeight: 700,
                mt: { xs: 3, xl: 2 },
              }}
            >
              <Box component="span" sx={{ mr: 1 }}>
                Already have an account?
              </Box>
              <Box
                component="span"
                sx={{
                  color: "primary.main",
                  fontWeight: 500,
                  cursor: "pointer",
                }}
                onClick={() => navigate(findPathByLabel(authRoutes, "SignIn"))}
              >
                Sign In Here
              </Box>
            </Box>
          </Grid>
        </Grid>
      </Card>
    </Box>
  );
}

export default SignUp;
