import { Button } from "@mantine/core";
import { login } from "@root/src/features/auth/service/auth";

export default function SignIn() {
  const onSignIn = async () => {
    await login();
  };
  return <Button onClick={onSignIn}>Sign in</Button>;
}
