import { Button, Text } from "@mantine/core";
import { triggerChannelImport } from "../../service/channelService";

export default function SuggestChannelImport() {
  const onClick = async () => {
    await triggerChannelImport();
  };

  return (
    <div className="w-full h-full grid content-center justify-center">
      <div className="flex flex-col gap-4 items-center">
        <Text size="xxl">No channels yet</Text>
        <Text size="xl">Do you want to import your channels?</Text>
        <Button onClick={onClick}>Import</Button>
      </div>
    </div>
  );
}
