import styles from "./GroupSubjectCreateForm.module.scss";
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
import Header from "../../../components/Admin/Header/Header";
import { useSnackbar } from "notistack";
import Autocomplete from "@mui/material/Autocomplete";
import Chip from "@mui/material/Chip";
// import { EventCreateSchema } from "@/validations/FormSchemas";
import useService from "../../../hooks";


  // const initialValues = {
  //   groupId: "",
  //   subjectId: "",
  //   teacherRole: [
  //       {
  //       teacherId: "",
  //       roleId: "",
  //       },
  //   ],
  //   credits: 0,
  //   totalHours: 0,
  // };

  const initialValues = {
    groupId: null,
    subjectId: null,
    teacherRole: [
        {
        teacherId: null,
        roleId: null,
        },
    ],
    credits: 0,
    totalHours: 0,
  };
  
  function GroupSubjectCreateForm() {
    const { enqueueSnackbar } = useSnackbar();
    const navigate = useNavigate();
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const [serverErrors, setServerErrors] = useState({});
    const [groups, setGroups] = useState([]);
    const [subjects, setSubjects] = useState([]);
    const [teachers, setTeachers] = useState([]);
    const [roles, setRoles] = useState([]);
    const { facultyServices } = useService();
    const { groupSubjectServices } = useService();
    const { groupServices } = useService();
    const { subjectServices } = useService();
    const { teacherServices } = useService();
    const { roleServices } = useService();
    const { teacherRoleServices } = useService();

    const token = localStorage.getItem("accessToken");

    if (token) {
      // Use the token in your code
      console.log("Token:", token);
    } else {
      // Handle the case where the token is not found
      console.error("Token not found in localStorage");
    }

    useEffect(() => {
        async function fetchData() {
          try {
            const response = await groupServices.getAllGroups(token);
            console.log(response);
            setGroups(response.data);
          } catch (error) {
            console.error("Error fetching groups:", error);
          }
        }
    
        fetchData();
      }, [token]);

      useEffect(() => {
        async function fetchData() {
          try {
            const response = await subjectServices.getAllSubjects(token);
            console.log(response);
            setSubjects(response.data);
          } catch (error) {
            console.error("Error fetching subjects:", error);
          }
        }
    
        fetchData();
      }, [token]);

      useEffect(() => {
        async function fetchData() {
          try {
            const response = await teacherServices.getAllTeachers(token);
            console.log(response);
            setTeachers(response.data);
          } catch (error) {
            console.error("Error fetching teachers:", error);
          }
        }
    
        fetchData();
      }, [token]);

      useEffect(() => {
        async function fetchData() {
          try {
            const response = await teacherRoleServices.getAllTeacherRoles(token);
            console.log(response);
            setRoles(response.data);
          } catch (error) {
            console.error("Error fetching roles:", error);
          }
        }
    
        fetchData();
      }, [token]);

    const handleFormSubmit = async (values) => {
      // const requestData = {
      //   name: values.name,
      //   facultyCode: values.facultyCode,
      //   studySectorName: values.studySectorName,
      //   studySectorCode: values.studySectorCode,
      // };

      const requestData = {
        groupId: values.groupId.id,
        subjectId: values.subjectId.id,
        teacherId: values.teacherId.id,
        roleId: values.roleId.id,
        credits: values.credits,
        totalHours: values.totalHours
      };
      const formData = new FormData();

    //   values.roleIds.forEach((role) => {
    //     formData.append("roleIds", role.id);
    //   });

      formData.append("groupId", values.groupId.id);
      formData.append("subjectId", values.subjectId.id);
      formData.append("teacherId", values.teacherId.id);
      formData.append("roleId", values.roleId.id);
      formData.append("credits", values.credits);
      formData.append("totalHours", values.totalHours);

      try {
        // Make your API request using formData
        // const response = await facultyServices.createFaculty( formData );
        const response = await groupSubjectServices.createGroupSubject( requestData );
    
        console.log("Success:", response.data);
        enqueueSnackbar("Group subject added successfully!", {
          variant: "success",
          autoHideDuration: 5000,
        });
        navigate(findPathByLabel(adminRoutes, "GroupSubjects"));
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
          enqueueSnackbar("Error adding group subject", { variant: "error" });
        }
      }
    };
    
  
    return (
      <Box m="20px">
      <Header title="Group subjects" subtitle="Create New Record" />
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
          setFieldValue,
          setFieldTouched,
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

                <Autocomplete
                    PaperComponent={({ children, ...other }) => (
                    <Paper
                        {...other}
                        sx={{ backgroundColor: colors.blueAccent[700] }}
                    >
                        {children}
                    </Paper>
                    )}
                    id="groupId"
                    options={groups}
                    // getOptionLabel={(group) => (group ? group.name : "")}
                    getOptionLabel={(group) => (group ? group.name : "")}

                    // value={values.appUserId}

                    // value={values.groupId || ''}
                    value={values.groupId || null}
                    // value={values.appUserId.id || null}

                    onChange={(_, newValue) => {
                    setFieldValue("groupId", newValue);
                    setFieldTouched("groupId", true); // Set the touch status
                    }}
                    onBlur={() => setFieldTouched("groupId", true)} // Ensure that field is marked touched on blur.
                    renderInput={(params) => (
                    <TextField
                        {...params}
                        variant="filled"
                        label="Select Group for Group Subject"
                        error={!!touched.groupId && !!errors.groupId}
                        helperText={touched.groupId && errors.groupId}
                        InputLabelProps={{
                        style: { fontSize: "1rem" },
                        focused: false,
                        }}
                        FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                    />
                    )}
                    renderTags={(selectedGroups, getTagProps) =>
                        selectedGroups.map((specRole, index) => (
                        <Chip
                        key={specRole.id} // using the unique project ID as the key
                        variant="outlined"
                        label={specRole.name} // displaying the project name on the chip
                        {...getTagProps({ index })}
                        />
                    ))
                    }
                />

                <Autocomplete
                    PaperComponent={({ children, ...other }) => (
                    <Paper
                        {...other}
                        sx={{ backgroundColor: colors.blueAccent[700] }}
                    >
                        {children}
                    </Paper>
                    )}
                    id="subjectId"
                    options={subjects}
                    getOptionLabel={(subject) => (subject ? subject.name : "")}

                    // value={values.appUserId}
                    // value={values.subjectId || ''}
                    value={values.subjectId || null}
                    // value={values.appUserId.id || null}

                    onChange={(_, newValue) => {
                    setFieldValue("subjectId", newValue);
                    setFieldTouched("subjectId", true); // Set the touch status
                    }}
                    onBlur={() => setFieldTouched("subjectId", true)} // Ensure that field is marked touched on blur.
                    renderInput={(params) => (
                    <TextField
                        {...params}
                        variant="filled"
                        label="Select Subject for Group Subject"
                        error={!!touched.subjectId && !!errors.subjectId}
                        helperText={touched.subjectId && errors.subjectId}
                        InputLabelProps={{
                        style: { fontSize: "1rem" },
                        focused: false,
                        }}
                        FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                    />
                    )}
                    renderTags={(selectedSubjects, getTagProps) =>
                        selectedSubjects.map((specRole, index) => (
                        <Chip
                        key={specRole.id} // using the unique project ID as the key
                        variant="outlined"
                        label={specRole.name} // displaying the project name on the chip
                        {...getTagProps({ index })}
                        />
                    ))
                    }
                />

                <Autocomplete
                    PaperComponent={({ children, ...other }) => (
                    <Paper
                        {...other}
                        sx={{ backgroundColor: colors.blueAccent[700] }}
                    >
                        {children}
                    </Paper>
                    )}
                    id="teacherId"
                    options={teachers}
                    getOptionLabel={(teacher) => (teacher ? teacher.name : "")}

                    // value={values.appUserId}
                    // value={values.teacherId || ''}
                    value={values.teacherId || null}
                    // value={values.appUserId.id || null}
                    
                    onChange={(_, newValue) => {
                    setFieldValue("teacherId", newValue);
                    setFieldTouched("teacherId", true); // Set the touch status
                    }}
                    onBlur={() => setFieldTouched("teacherId", true)} // Ensure that field is marked touched on blur.
                    renderInput={(params) => (
                    <TextField
                        {...params}
                        variant="filled"
                        label="Select Teacher for Group Subject"
                        error={!!touched.teacherId && !!errors.teacherId}
                        helperText={touched.teacherId && errors.teacherId}
                        InputLabelProps={{
                        style: { fontSize: "1rem" },
                        focused: false,
                        }}
                        FormHelperTextProps={{ style: { fontSize: "0.9rem" } }} // Increase font size of helperText
                    />
                    )}
                    renderTags={(selectedTeachers, getTagProps) =>
                        selectedTeachers.map((specRole, index) => (
                        <Chip
                        key={specRole.id} // using the unique project ID as the key
                        variant="outlined"
                        label={specRole.name} // displaying the project name on the chip
                        {...getTagProps({ index })}
                        />
                    ))
                    }
                />

                <Autocomplete
                    PaperComponent={({ children, ...other }) => (
                    <Paper
                        {...other}
                        sx={{ backgroundColor: colors.blueAccent[700] }}
                    >
                        {children}
                    </Paper>
                    )}
                    id="roleId"
                    options={roles}
                    getOptionLabel={(role) => (role ? role.name : "")}

                    // value={values.appUserId}
                    // value={values.roleId || ''}
                    value={values.roleId || null}
                    // value={values.appUserId.id || null}

                    onChange={(_, newValue) => {
                    setFieldValue("roleId", newValue);
                    setFieldTouched("roleId", true); // Set the touch status
                    }}
                    onBlur={() => setFieldTouched("roleId", true)} // Ensure that field is marked touched on blur.
                    renderInput={(params) => (
                    <TextField
                        {...params}
                        variant="filled"
                        label="Select Role for Group Subject"
                        error={!!touched.roleId && !!errors.roleId}
                        helperText={touched.roleId && errors.roleId}
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
                />

              {/* Credits */}
              <TextField
                fullWidth
                variant="filled"
                type="number"
                label="Credits"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.credits || ""}
                name="credits"
                error={!!touched.credits && !!errors.credits}
                helperText={touched.credits && errors.credits}
              />

              {/* Total Hours */}
              <TextField
                fullWidth
                variant="filled"
                type="number"
                label="Total Hours"
                onBlur={handleBlur}
                onChange={handleChange}
                value={values.totalHours || ""}
                name="totalHours"
                error={!!touched.totalHours && !!errors.totalHours}
                helperText={touched.totalHours && errors.totalHours}
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

  export default GroupSubjectCreateForm;