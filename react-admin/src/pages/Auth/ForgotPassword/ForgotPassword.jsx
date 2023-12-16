import styles from "./ForgotPassword.module.scss";
import React from "react";
import {
  useTheme,
  TextField,
  Card,
  Button,
  Grid,
  Box,
  Typography,
} from "@mui/material";
import { Form, Formik, useField, Field } from "formik";
import { ReactComponent as Logo } from "../../../assets/icons/forgot-password.svg";
import { ForgotPasswordSchema } from "../../../validations/FormSchemas";
import authRoutes from "../../../routing/authRoutes";
import axios from "axios";
// import { ACCOUNT_FORGOT_PASSWORD_URL } from "@/constants/Urls";
import useService from "../../../hooks";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "notistack";
import { findPathByLabel } from "../../../utils/routingUtils";

const AppTextField = (props) => {
  const [field, meta] = useField(props);
  const errorText = meta.error && meta.touched ? meta.error : " ";
  return (
    <TextField
      {...props}
      {...field}
      helperText={errorText}
      error={meta.touched && !!meta.error}
      sx={{
        width: "100%",
        mb: 1,
      }}
    />
  );
};

const initialValues = {
  email: "",
};

function ForgotPassword() {
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();
  const theme = useTheme();
  const [serverErrors, setServerErrors] = React.useState({});
  const { authServices } = useService();

  const handleFormSubmit = async (
    values,
    { setSubmitting, setErrors, resetForm }
  ) => {
    setSubmitting(true);

    try {
      const response = await authServices.forgotPassword({
        email: values.email,
      });

      console.log("Forgot Password successful:", response.data);
      console.log("Full API Response:", response);
      enqueueSnackbar("Instructions sent! Check your email.", {
        variant: "success",
        autoHideDuration: 5000,
      });
      navigate(findPathByLabel(authRoutes, "ResetPasswordMessage"));
    } catch (error) {
      let errorMessage = "An error occurred while sending the instructions.";
        if (error.response) {
          if (error.response.status === 500) {
            navigate("/error500");
            return;
          } else if (error.response.data && error.response.data.errors) {
            const errorsFromServer = error.response.data.errors;
            setErrors(errorsFromServer);
            setServerErrors(errorsFromServer); // Store the server errors
            console.log(errorsFromServer);
            errorMessage = "Please correct the highlighted errors.";
          }
        }

        enqueueSnackbar(errorMessage, {
          variant: "error",
        });
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <Box
      sx={{
        padding: "1rem",
        display: "flex",
        minHeight: "100vh",
        flex: 1,
        width: "100%",
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
              Forgot Your Password?
            </Box>

            <Typography variant="body1" sx={{ mb: 3 }}>
              Forgot your password? No need to worry. Tell us your email and we
              will send you instructions on how to reset it.
            </Typography>

            {Object.keys(serverErrors).length > 0 && (
              <Box
                bgcolor="error.main"
                color="white"
                p={2}
                borderRadius={2}
                sx={{
                  mb: { xs: 6, xl: 3 },
                }}
              >
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
              validateOnChange={true}
              initialValues={initialValues}
              validationSchema={ForgotPasswordSchema}
              onSubmit={handleFormSubmit}
            >
              {({ isSubmitting, values, handleChange }) => (
                <Form noValidate autoComplete="on">
                  <Grid container spacing={1}>
                    <Grid item xs={12}>
                      <AppTextField
                        label="Email"
                        name="email"
                        variant="outlined"
                        autoComplete="email"
                      />
                    </Grid>
                  </Grid>
                  <Box
                    sx={{
                      mb: { xs: 5, xl: 2 },
                      display: "flex",
                      flexWrap: "wrap",
                      alignItems: "center",
                      justifyContent: "space-between",
                    }}
                  ></Box>
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
                    Send Request
                  </Button>
                </Form>
              )}
            </Formik>
          </Grid>
        </Grid>
      </Card>
    </Box>
  );
}

export default ForgotPassword;